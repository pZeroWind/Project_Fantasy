/*
 * 文件名：StateMachineEditor.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/17
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/18
 * 
 * 文件描述：
 * 状态机XML工具 用于生成xml文件与生成xsd文件
 */

using Framework.Runtime.Behavior;
using Framework.Runtime.States;
using Framework.Units;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    public class StateMachineEditor
    {
        private readonly static string ResRoot = Application.dataPath + "/Resources/StateMachine";
        private static List<XmlSchemaElement> _conditionElements;
        private static List<XmlSchemaElement> _actionElements;
        private static List<XmlSchemaElement> _translationElements;

        [MenuItem("状态机/创建状态")]
        public static void OnCreateXML()
        {
            XNamespace ns = "http://example.com/stateMachine";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            List<XElement> list = new List<XElement>
            {
                new(ns + "State",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(xsi + "schemaLocation", "http://example.com/stateMachine stateMachineSchema.xsd"),
                    string.Empty
                    )
            };
            XMLHelper.XmlSave(list, $"Resources/StateMachine/NewState[{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}]");
            AssetDatabase.Refresh();
        }

        [MenuItem("状态机/编译节点")]
        public static void OnReloadNode()
        {
            XmlSchema schema = CreateSchema();
            // 将 schema 保存到文件
            string filePath = $"{ResRoot}/stateMachineSchema.xsd"; // 要保存的文件路径
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                schema.Write(fileStream);
            }
            AssetDatabase.Refresh();
        }

        public static XmlSchema CreateSchema()
        {
            List<Type> _actionTypes = new List<Type>();
            List<Type> _conditionTypes = new List<Type>();
            List<Type> _translationTypes = new List<Type>();
            XmlSchema mainSchema = new XmlSchema
            {
                TargetNamespace = "http://example.com/stateMachine",
                ElementFormDefault = XmlSchemaForm.Qualified
            };
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                _actionTypes.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(ActionEvent))));
                _conditionTypes.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(ConditionEvent))));
                _translationTypes.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract &&
                    (t.IsSubclassOf(typeof(TranslationEvent)) || t == typeof(TranslationEvent))));
            }

            var tree = new XmlSchemaElement();
            tree.Name = "State";
            var compositeNodeType = new XmlSchemaComplexType();
            var sequence = new XmlSchemaSequence();
            compositeNodeType.Particle = sequence;
            // 定义可以包含任意类型的子元素
            var anyElement = new XmlSchemaAny
            {
                MinOccurs = 0,
                MaxOccursString = "unbounded",
                ProcessContents = XmlSchemaContentProcessing.Lax // 宽松验证
            };
            sequence.Items.Add(anyElement);
            tree.SchemaType = compositeNodeType;


            // 将元素添加到 schema 中
            mainSchema.Items.Add(tree);

            

            _conditionElements = new List<XmlSchemaElement>();
            _actionElements = new List<XmlSchemaElement>();
            _translationElements = new List<XmlSchemaElement>();
            foreach (var type in _conditionTypes)
            {
                _conditionElements.Add(BuildType(type, mainSchema));
            }

            foreach (var type in _actionTypes)
            {
                _actionElements.Add(BuildType(type, mainSchema));
            }

            foreach (var type in _translationTypes)
            {
                _translationElements.Add(BuildType(type, mainSchema));
            }
            BuildNode("ActionEvents", mainSchema, _actionElements);
            BuildNode("TranslationEvents", mainSchema, _translationElements);
            return mainSchema;
        }

        private static void BuildNode(string name, XmlSchema mainSchema, List<XmlSchemaElement> elements)
        {
            var element = new XmlSchemaElement();
            element.Name = name;
            var composite = new XmlSchemaComplexType();
            var seq = new XmlSchemaSequence();
            composite.Particle = seq;
            foreach (var item in elements) 
                seq.Items.Add(item);
            element.SchemaType = composite;
            // 将元素添加到 schema 中
            mainSchema.Items.Add(element);
        }

        private static XmlSchemaElement BuildType(Type type,  XmlSchema mainSchema)
        {
            var element = new XmlSchemaElement();
            element.Name = type.Name;
            element.MinOccurs = 0;
            element.MaxOccursString = "unbounded";
            var composite = new XmlSchemaComplexType();
            var seq = new XmlSchemaSequence();
            composite.Particle = seq;
            // 定义只允许包含其字段的子元素
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(List<ConditionEvent>))
                {
                    var compositefield = new XmlSchemaComplexType();
                    var seqfield = new XmlSchemaSequence();
                    compositefield.Particle = seqfield;
                    foreach (var ele in _conditionElements)
                        seqfield.Items.Add(ele);
                    var fieldEle = new XmlSchemaElement
                    {
                        Name = field.Name,
                        MinOccurs = 0,
                        SchemaType = compositefield
                    };
                    seq.Items.Add(fieldEle);
                }
                else if (field.FieldType.GetInterfaces().Any(i =>i == typeof(IEvent)))
                {
                    var compositefield = new XmlSchemaComplexType();
                    var seqfield = new XmlSchemaSequence();
                    compositefield.Particle = seqfield;
                    foreach (var ele in _actionElements)
                    {
                        if(ele.Name == field.FieldType.Name)
                            seqfield.Items.Add(ele);
                    }
                    var fieldEle = new XmlSchemaElement
                    {
                        Name = field.Name,
                        MinOccurs = 0,
                        SchemaType = compositefield
                    };
                    seq.Items.Add(fieldEle);
                }
                else
                {
                    var fieldEle = new XmlSchemaElement
                    {
                        Name = field.Name,
                        MinOccurs = 0,
                    };
                    seq.Items.Add(fieldEle);
                }
            }
            element.SchemaType = composite;

            return element;
        }
    }
}

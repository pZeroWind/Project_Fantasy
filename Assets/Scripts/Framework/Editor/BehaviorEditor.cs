/*
 * 文件名：BehaviorEditor.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/17
 * 
 * 文件描述：
 * 行为树XML工具 用于生成xml文件与生成xsd文件
 */

using Framework.Runtime.Behavior;
using Framework.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    public class BehaviorEditor
    {
        private readonly static string ResRoot = Application.dataPath + "/Resources/BehaviorTrees";

        [MenuItem("行为树/创建行为树")]
        public static void OnCreateXML()
        {
            XNamespace ns = "http://example.com/behaviorTree";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            List<XElement> list = new List<XElement>
            {
                new(ns + "Tree",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(xsi + "schemaLocation", "http://example.com/behaviorTree behaviorTreeSchema.xsd"),
                    string.Empty
                    )
            };
            XMLHelper.XmlSave(list, $"Resources/BehaviorTrees/NewTree[{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}]");
            AssetDatabase.Refresh();
        }

        [MenuItem("行为树/编译节点")]
        public static void OnReloadNode()
        {
            XmlSchema schema = CreateSchema();
            // 将 schema 保存到文件
            string filePath = $"{ResRoot}/behaviorTreeSchema.xsd"; // 要保存的文件路径
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                schema.Write(fileStream);
            }
            AssetDatabase.Refresh();
        }

        public static XmlSchema CreateSchema()
        {
            List<Type> _types = new List<Type>();
            XmlSchema mainSchema = new XmlSchema
            {
                TargetNamespace = "http://example.com/behaviorTree",
                ElementFormDefault = XmlSchemaForm.Qualified
            };
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                _types.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(BehaviorNode))));
            }

            var tree = new XmlSchemaElement();
            tree.Name = "Tree";
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

            foreach (var type in _types)
            {
                var element = new XmlSchemaElement();
                element.Name = type.Name;
                var composite = new XmlSchemaComplexType();
                var seq = new XmlSchemaSequence();
                composite.Particle = seq;
                // 定义可以包含任意类型的子元素
                var anyEle = new XmlSchemaAny
                {
                    MinOccurs = 0,
                    MaxOccursString = "unbounded",
                    ProcessContents = XmlSchemaContentProcessing.Lax // 宽松验证
                };
                seq.Items.Add(anyEle);
                element.SchemaType = compositeNodeType;


                // 将元素添加到 schema 中
                mainSchema.Items.Add(element);
            }

            return mainSchema;
        }
    }
}

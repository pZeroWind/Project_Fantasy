/*
 * 文件名：StateMachineManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 状态机管理器类
 */

using Framework.Runtime.States;
using Framework.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Framework.Runtime
{
    public class StateMachineManager : Singleton<StateMachineManager>
    {
        private const string ResRoot = "StateMachine";

        private readonly List<Type> _types;

        public readonly Dictionary<string, List<State>> _stateMachines = new Dictionary<string, List<State>>();

        public StateMachineManager() 
        {
            _types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                _types.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(ActionEvent))));
                _types.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(ConditionEvent))));
                _types.AddRange(assembly.GetTypes().Where(t => !t.IsAbstract &&
                    (t.IsSubclassOf(typeof(TranslationEvent)) || t == typeof(TranslationEvent))));
            }
        }

        public List<State> OnLoad(string statesName)
        {
            IEnumerable<ConditionEvent> LoadCondition(XElement root)
            {
                foreach (var condition in root.Elements())
                {
                    var type = _types.FirstOrDefault(t => t.Name == condition.Name.LocalName);
                    var obj = Activator.CreateInstance(type);
                    var fields = type.GetFields();
                    foreach (var field in fields)
                    {
                        try
                        {
                            var val = condition.Elements().FirstOrDefault(c => c.Name.LocalName == field.Name);
                            if (val != null)
                            {
                                var value = Convert.ChangeType(val.Value, field.FieldType);
                                field.SetValue(obj, value);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"状态机生成时类型转换错误:{e.Message}");
                        }
                        yield return (ConditionEvent)obj;
                    }
                }
            }
            T LoadEvent<T>(XElement xml, Type type)
            {
                var obj = Activator.CreateInstance(type);
                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(List<ConditionEvent>))
                    {
                        var val = xml.Elements().FirstOrDefault(c => c.Name.LocalName == field.Name);
                        if (val != null)
                        {
                            field.SetValue(obj, LoadCondition(val).ToList());
                        }
                        continue;
                    }
                    try
                    {
                       
                        var val = xml.Elements().FirstOrDefault(c => c.Name.LocalName == field.Name);
                        if (val != null)
                        {
                            var value = Convert.ChangeType(val.Value, field.FieldType);
                            field.SetValue(obj, value);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"状态机生成时类型转换错误:{e.Message}");
                    }
                }
                return (T)obj;
            }

            var textAssets = GameResourceManager.Instance.LoadAll<TextAsset>($"StateMachine/{statesName}");
            var states = new List<State>();
            foreach (var textAsset in textAssets)
            {
                var xml = XMLHelper.Parse(textAsset);
                State state = new State();
                state.StateName = textAsset.name;
                foreach(var stateObj in xml.Elements())
                {
                    Debug.Log(stateObj.Name.LocalName);
                }
                var actionXml = xml.Elements().FirstOrDefault(x => x.Name.LocalName == "ActionEvents");
                foreach (var action in actionXml.Elements())
                {
                    var type = _types.FirstOrDefault(t => t.Name == action.Name.LocalName);
                    state.ActionEvents.Add(LoadEvent<ActionEvent>(action, type));
                }

                var translationXml = xml.Elements().FirstOrDefault(x => x.Name.LocalName == "TranslationEvents");
                foreach (var translation in translationXml.Elements())
                {
                    var type = _types.FirstOrDefault(t => t.Name == translation.Name.LocalName);
                    state.TranslationEvents.Add(LoadEvent<TranslationEvent>(translation, type));
                }

                states.Add(state);
            }

            return states;
        }

        public IEnumerable<State> LoadStateMachine(string name)
        {
            // 若缓存中不存在状态机则从资源中加载并创建一个新的状态机副本
            if(!_stateMachines.TryGetValue(name, out var states))
            {
                states = OnLoad(name);
                _stateMachines.Add(name, states);
            }
            return states.Select(state => state.Clone());
        }
    }
}


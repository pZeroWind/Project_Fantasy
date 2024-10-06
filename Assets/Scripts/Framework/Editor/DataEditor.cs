/*
 * 文件名：DataEditor.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/1
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/4
 * 
 * 文件描述：
 * 编辑器工具 用于编辑游戏内各项数据
 */

using Framework.Runtime;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    public enum DataEditorType
    {
        Entity,
        Buff,
        Item
    }

    public class DataEditor : EditorWindow
    {

        #region 实体数据
        private List<EntityData> _entities = new List<EntityData>();

        private List<EntityDataType> _entityTypes = new List<EntityDataType>();

        private EntityDataType _currentEntityType = EntityDataType.Default;

        private string _currentEntityId = string.Empty;
        #endregion

        #region Buff数据
        private List<BuffData> _buffs = new List<BuffData>();

        private List<BuffType> _buffTypes = new List<BuffType>();

        private BuffType _currentBuffType = BuffType.None;

        private string _currentBuffId = string.Empty;
        #endregion

        private Vector2 _currentPosition = Vector2.zero;

        private DataEditorType _editorType = DataEditorType.Entity;

        private bool _json = false;

        [MenuItem("游戏数据/实体数据")]
        public static void OnLoadEntityDataEditor()
        {
            DataEditor editor = EditorWindow.GetWindow<DataEditor>("实体数据编辑器");
            editor.OnInit(DataEditorType.Entity);
            editor.Show();
        }

        [MenuItem("游戏数据/Buff数据")]
        public static void OnLoadBuffDataEditor()
        {
            DataEditor editor = EditorWindow.GetWindow<DataEditor>("Buff数据编辑器");
            editor.OnInit(DataEditorType.Buff);
            editor.Show();
        }

        public void OnInit(DataEditorType type)
        {
            _editorType = type;
            position = new Rect(100, 100, 800, 450);
            switch (_editorType)
            {
                case DataEditorType.Entity:
                    {
                        var textArr = Resources.LoadAll<TextAsset>("Data/EntityData");
                        foreach (var txt in textArr)
                        {
                            var json = JObject.Parse(txt.text);
                            switch (Enum.Parse<EntityDataType>(json[nameof(EntityDataType)].ToString()))
                            {
                                default:
                                case EntityDataType.Default:
                                    {
                                        var e = new EntityData();
                                        e.Deserialize(json);
                                        _entities.Add(e);
                                    }
                                    break;
                                case EntityDataType.Character:
                                    {
                                        var e = new CharacterEntityData();
                                        e.Deserialize(json);
                                        _entities.Add(e);
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case DataEditorType.Buff:
                    {
                        var textArr = Resources.LoadAll<TextAsset>("Data/BuffData");
                        foreach (var txt in textArr)
                        {
                            var json = JObject.Parse(txt.text);
                            switch (Enum.Parse<BuffType>(json[nameof(BuffType)].ToString()))
                            {
                                default:
                                case BuffType.None:
                                    {
                                        var e = new BuffData();
                                        e.Deserialize(json);
                                        _buffs.Add(e);
                                    }
                                    break;
                                case BuffType.NumericBuff:
                                    {
                                        var e = new BuffData();
                                        e.Deserialize(json);
                                        _buffs.Add(e);
                                    }
                                    break;
                                case BuffType.DotBuff:
                                    break;
                                case BuffType.ModifyBuff:
                                    break;
                            }
                        }
                    }
                    break;
                case DataEditorType.Item:
                    break;
            }
        }

        private void OnEnable()
        {
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            switch (_editorType)
            {
                case DataEditorType.Entity:
                    foreach (var entity in _entities)
                    {
                        if (!_entityTypes.Contains(entity.EntityDataType))
                        {
                            _entityTypes.Add(entity.EntityDataType);
                            GUIStyle typeStyle = new()
                            {
                                fontStyle = FontStyle.Bold,
                                fontSize = 14,
                                fixedWidth = 200,
                                alignment = TextAnchor.MiddleLeft,
                                padding = new RectOffset(4, 2, 2, 2),
                                normal = new GUIStyleState()
                                {
                                    background = Texture2D.grayTexture
                                },
                                active = new GUIStyleState()
                                {
                                    background = Texture2D.whiteTexture
                                }
                            };
                            if (GUILayout.Button(entity.EntityDataType.ToString(), typeStyle))
                            {
                                _currentEntityType = entity.EntityDataType;
                            }
                        }
                        if (_currentEntityType == entity.EntityDataType)
                        {
                            GUIStyle childStyle = new()
                            {
                                fontStyle = FontStyle.Normal,
                                fontSize = 12,
                                fixedWidth = 200,
                                alignment = TextAnchor.MiddleLeft,
                                padding = new RectOffset(4, 2, 2, 2),
                                normal = new GUIStyleState()
                                {
                                    background = Texture2D.grayTexture
                                },
                                active = new GUIStyleState()
                                {
                                    background = Texture2D.whiteTexture
                                },
                            };
                            GUIStyle childStyle_Selected = new()
                            {
                                fontStyle = FontStyle.Italic,
                                fontSize = 12,
                                fixedWidth = 200,
                                alignment = TextAnchor.MiddleLeft,
                                padding = new RectOffset(4, 2, 2, 2),
                                normal = new GUIStyleState()
                                {
                                    background = Texture2D.whiteTexture
                                },
                            };
                            if (GUILayout.Button($"{entity.EntityId}", _currentEntityId != entity.EntityId ? childStyle : childStyle_Selected))
                            {
                                _currentEntityId = entity.EntityId;
                            }
                        }
                    }
                    break;
                case DataEditorType.Buff:
                    break;
                case DataEditorType.Item:
                    break;
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            _currentPosition = EditorGUILayout.BeginScrollView(_currentPosition);
            var e = _entities.FirstOrDefault(en => en.EntityId == _currentEntityId);
            if (e != null)
            {
                ShowData("基本数据", e);
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        public void ShowData(string name, object data)
        {
            Queue<(string Name, object Obj)> queue = new Queue<(string, object)> ();
            queue.Enqueue((name, data));
            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                GUIStyle textStyle = new()
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 14,
                    fixedWidth = 200,
                    alignment = TextAnchor.MiddleLeft,
                };
                GUILayout.Label(cur.Name, textStyle);
                Type t = cur.Obj.GetType();
                var fields = t.GetFields();
                foreach (var field in fields)
                {
                    EditorGUILayout.BeginHorizontal();
                    var fieldValue = field.GetValue(cur.Obj);
                    if (fieldValue is IDataProperty)
                    {
                        queue.Enqueue((field.Name, fieldValue));
                    }
                    else
                    {
                        GUILayout.Label(field.Name);
                        var jsonField = field.GetCustomAttribute<JsonFieldAttribute>();
                        switch (jsonField.DataType)
                        {
                            case JsonType.String:
                                field.SetValue(
                                    cur.Obj,
                                    EditorGUILayout.TextField(fieldValue.ToString(),
                                    GUILayout.Width(300)));
                                break;
                            case JsonType.Enum:
                                field.SetValue(cur.Obj,
                                    EditorGUILayout.EnumPopup(
                                        (Enum)Enum.Parse(field.FieldType, fieldValue.ToString()),
                                        GUILayout.Width(300)
                                    )
                                );
                                break;
                            case JsonType.Bool:
                                field.SetValue(
                                    cur.Obj,
                                    EditorGUILayout.Toggle((bool)fieldValue, field.Name));
                                break;
                            case JsonType.Int:
                                field.SetValue(
                                    cur.Obj,
                                   EditorGUILayout.IntField((int)fieldValue,
                                    GUILayout.Width(300)));
                                break;
                            case JsonType.Float:
                                field.SetValue(
                                    cur.Obj,
                                    EditorGUILayout.FloatField((float)fieldValue,
                                    GUILayout.Width(300)));
                                break;
                            case JsonType.Double:
                                field.SetValue(
                                    cur.Obj,
                                    EditorGUILayout.DoubleField((double)fieldValue,
                                    GUILayout.Width(300)));
                                break;
                            case JsonType.Object:
                                break;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            if (!_json)
            {
                if (GUILayout.Button("查看JSON"))
                {
                    _json = !_json;
                }
            }
            else if (_json)
            {
                if (GUILayout.Button("隐藏JSON"))
                {
                    _json = !_json;
                }
                var e = _entities.FirstOrDefault(en => en.EntityId == _currentEntityId);
                if (e != null)
                {
                    var json = e.Serialize();
                    GUILayout.TextArea(json.ToString());
                }
            }
            if (GUILayout.Button("保存当前数据"))
            {

            }
        }
    }
}


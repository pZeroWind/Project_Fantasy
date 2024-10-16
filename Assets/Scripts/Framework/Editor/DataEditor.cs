/*
 * 文件名：DataEditor.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/1
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 编辑器工具 用于编辑游戏内各项数据
 */

using Framework.Runtime;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        private List<JObject> jsonArray = new List<JObject>();
        #endregion

        private int _currentIndex = -1;

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

        [MenuItem("游戏数据/BUFF数据")]
        public static void OnLoadBuffDataEditor()
        {
            DataEditor editor = EditorWindow.GetWindow<DataEditor>("BUFF数据编辑器");
            editor.OnInit(DataEditorType.Buff);
            editor.Show();
        }

        public void OnInit(DataEditorType type)
        {
            AssetDatabase.Refresh();
            _currentIndex = -1;
            _editorType = type;
            position = new Rect(100, 100, 800, 450);
            jsonArray.Clear();
            switch (_editorType)
            {
                case DataEditorType.Entity:
                    {
                        var textArr = Resources.LoadAll<TextAsset>("Data/EntityData");
                        foreach (var txt in textArr)
                        {
                            var json = JObject.Parse(txt.text);
                            jsonArray.Add(json);
                        }
                    }
                    break;
                case DataEditorType.Buff:
                    {
                        var textArr = Resources.LoadAll<TextAsset>("Data/BuffData");
                        foreach (var txt in textArr)
                        {
                            var json = JObject.Parse(txt.text);
                            jsonArray.Add(json);
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
            EditorGUILayout.BeginVertical(GUILayout.Width(200));
            EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
            if (GUILayout.Button("创建数据"))
            {
                JObject obj = new JObject();
                jsonArray.Add(obj);
            }
            if (GUILayout.Button("删除数据"))
            {

            }
            if (GUILayout.Button("刷新数据"))
            {
                OnInit(_editorType);
            }
            GUILayout.EndHorizontal();
            foreach (var jsonObject in jsonArray)
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
                    fontStyle = FontStyle.Bold,
                    fontSize = 12,
                    fixedWidth = 200,
                    alignment = TextAnchor.MiddleLeft,
                    padding = new RectOffset(4, 2, 2, 2),
                    normal = new GUIStyleState()
                    {
                        background = Texture2D.whiteTexture
                    },
                };
                if (GUILayout.Button($"[{GetId(jsonObject)}]{GetName(jsonObject)}", _currentIndex != jsonArray.IndexOf(jsonObject) ? childStyle : childStyle_Selected))
                {
                    _currentIndex = jsonArray.IndexOf(jsonObject);
                }

            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            _currentPosition = EditorGUILayout.BeginScrollView(_currentPosition);
            if (_currentIndex > -1)
            {
                var e = jsonArray[_currentIndex];
                if (e != null)
                {
                    ShowData("基本数据", e);
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        public string GetId(JObject json)
        {
            return _editorType switch
            {
                DataEditorType.Entity => json["EntityId"] == null ? "" : json["EntityId"].ToString(),
                DataEditorType.Buff => json["BuffId"] == null ? "" : json["BuffId"].ToString(),
                DataEditorType.Item => json["ItemId"] == null ? "" : json["ItemId"].ToString(),
                _ => json["Id"] == null ? "" : json["Id"].ToString()
            };
        }

        public string GetName(JObject json)
        {
            return _editorType switch
            {
                DataEditorType.Entity => json["Name"] == null ? "" : json["Name"].ToString(),
                DataEditorType.Buff => json["Name"] == null ? "" : json["Name"].ToString(),
                DataEditorType.Item => json["Name"] == null ? "" : json["Name"].ToString(),
                _ => json["Name"] == null ? "" : json["Name"].ToString()
            };
        }

        public void ShowData(string name, JObject data)
        {
            var bindType = GetBindType(data);
            Queue<(string Name, JObject Obj, Type type)> queue = new Queue<(string, JObject, Type type)> ();
            queue.Enqueue((name, data, bindType));
            while (queue.Count > 0)
            {
                var (curName, curObj, curBindType) = queue.Dequeue();
                GUIStyle textStyle = new()
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 14,
                    fixedWidth = 200,
                    alignment = TextAnchor.MiddleLeft,
                };
                GUIStyle innerStyle = new()
                {
                    fontStyle = FontStyle.Normal,
                    fontSize = 12,
                    fixedWidth = 200,
                    alignment = TextAnchor.MiddleLeft,
                };
                GUILayout.Label(curName, textStyle);
                var fields = Units.JsonHelper.GetFieldInfoArr(curBindType);
                foreach (var field in fields)
                {
                    var jField = field.GetCustomAttribute<JsonFieldAttribute>();
                    if (jField == null) continue;
                    var jName = field.Name;
                    var fVal = curObj;
                    if (jField.DataType != JsonType.Object)
                        GUILayout.Label(jField.Name, innerStyle);
                    switch (jField.DataType)
                    {
                        default:
                        case JsonType.String:
                            if (curObj[jName] == null) curObj.Add(jName, string.Empty);
                            curObj[jName] = EditorGUILayout.TextField(curObj[jName].ToString());
                            break;
                        case JsonType.Enum:
                            if (curObj[jName] == null) curObj.Add(jName, Enum.GetName(field.FieldType, 0));
                            curObj[jName] = EditorGUILayout.EnumPopup(
                                (Enum)Enum.Parse(field.FieldType, curObj[jName].ToString()))
                                .ToString();
                            break;
                        case JsonType.Bool:
                            if (curObj[jName] == null) curObj.Add(jName, false);
                            curObj[jName] = EditorGUILayout.Toggle(curObj[jName].Value<bool>());
                            break;
                        case JsonType.Int:
                            if (curObj[jName] == null) curObj.Add(jName, 0);
                            curObj[jName] = EditorGUILayout.IntField(curObj[jName].Value<int>());
                            break;
                        case JsonType.Float:
                            if (curObj[jName] == null) curObj.Add(jName, 0f);
                            curObj[jName] = EditorGUILayout.FloatField(curObj[jName].Value<float>());
                            break;
                        case JsonType.Double:
                            if (curObj[jName] == null) curObj.Add(jName, 0d);
                            curObj[jName] = EditorGUILayout.DoubleField(curObj[jName].Value<double>());
                            break;
                        case JsonType.Object:
                            {
                                if (curObj[jName] == null) curObj.Add(jName, new JObject());
                                queue.Enqueue((jField.Name, (JObject)curObj[jName], field.FieldType));
                            }
                            break;
                        //case JsonType.GameObject:
                        //    {
                        //        if (curObj[jName] == null) curObj.Add(jName, string.Empty);
                        //        curObj[jName] = EditorGUILayout.ObjectField("选择预制体", Resources.Load<GameObject>(curObj[jName].ToString()), typeof(GameObject), false).GetInstanceID();
                        //    }
                        //    break;
                    }
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
                var json = jsonArray[_currentIndex];
                GUILayout.TextArea(json.ToString());
            }
            if (GUILayout.Button("保存当前数据"))
            {
                OnSaveData(jsonArray[_currentIndex]);
            }
        }

        private void OnSaveData(JObject data)
        {
            var json = new JObject();
            var bindType = GetBindType(data);
            Queue<(string Name, JObject SourceObj, JObject TargetObj, Type type)> queue = 
                new Queue<(string, JObject, JObject, Type type)>();
            queue.Enqueue((name, data, json, bindType));
            while (queue.Count > 0)
            {
                var (curName, sourObj, tarObj, curBindType) = queue.Dequeue();
                var fields = Units.JsonHelper.GetFieldInfoArr(curBindType);
                foreach (var field in fields)
                {
                    var jField = field.GetCustomAttribute<JsonFieldAttribute>();
                    if (jField == null) continue;
                    var jName = field.Name;
                    var fVal = sourObj;
                    switch (jField.DataType)
                    {
                        default:
                        case JsonType.String:
                            if (tarObj[jName] == null) tarObj.Add(jName, string.Empty);
                            tarObj[jName] = sourObj[jName].ToString();
                            break;
                        case JsonType.Enum:
                            if (tarObj[jName] == null) tarObj.Add(jName, Enum.GetName(field.FieldType, 0));
                            tarObj[jName] = Enum.Parse(field.FieldType, sourObj[jName].ToString()).ToString();
                            break;
                        case JsonType.Bool:
                            if (tarObj[jName] == null) tarObj.Add(jName, false);
                            tarObj[jName] = sourObj[jName].Value<bool>();
                            break;
                        case JsonType.Int:
                            if (tarObj[jName] == null) tarObj.Add(jName, 0);
                            tarObj[jName] = sourObj[jName].Value<int>();
                            break;
                        case JsonType.Float:
                            if (tarObj[jName] == null) tarObj.Add(jName, 0f);
                            tarObj[jName] = sourObj[jName].Value<float>();
                            break;
                        case JsonType.Double:
                            if (tarObj[jName] == null) tarObj.Add(jName, 0d);
                            tarObj[jName] = sourObj[jName].Value<double>();
                            break;
                        case JsonType.Object:
                            {
                                if (tarObj[jName] == null) tarObj.Add(jName, new JObject());
                                queue.Enqueue((jName, (JObject)sourObj[jName], (JObject)tarObj[jName], field.FieldType));
                            }
                            break;
                    }
                }
            }
            string path = Application.dataPath;

            using (StreamWriter sw = new StreamWriter(_editorType switch
            {
                DataEditorType.Entity => $"{path}/Resources/Data/EntityData/entity[{GetId(json)}].json",
                DataEditorType.Buff => $"{path}/Resources/Data/BuffData/buff[{GetId(json)}].json",
                DataEditorType.Item => $"{path}/Resources/Data/ItemData/item[{GetId(json)}].json",
                _ => path,
            }, false, Encoding.UTF8))
            {
                sw.Write(json.ToString());
                Debug.Log("Data File Save Success");
                AssetDatabase.Refresh();
            }
        }

        private Type GetBindType(JObject data)
        {
            var propType = _editorType switch
            {
                DataEditorType.Entity => typeof(EntityData)
                    .GetFields()
                    .FirstOrDefault(f => {
                        var attr = f.GetCustomAttribute<JsonFieldAttribute>();
                        if (attr == null) return false;
                        return attr.DataType == JsonType.Enum;
                    }),
                DataEditorType.Buff => typeof(BuffData)
                    .GetFields()
                    .FirstOrDefault(f => {
                        var attr = f.GetCustomAttribute<JsonFieldAttribute>();
                        if (attr == null) return false;
                        return attr.DataType == JsonType.Enum;
                    }),
                DataEditorType.Item => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };

            return _editorType switch
            {
                DataEditorType.Entity => typeof(EntityDataType)
                    .GetField(data[propType.Name] == null ?
                            EntityDataType.Default.ToString() :
                            data[propType.Name].ToString())
                    .GetCustomAttribute<ETypeBindingAttribute>()
                    .BindingType,
                DataEditorType.Buff => typeof(BuffDataType)
                    .GetField(data[propType.Name] == null ?
                            BuffDataType.None.ToString() :
                            data[propType.Name].ToString())
                    .GetCustomAttribute<ETypeBindingAttribute>()
                    .BindingType,
                DataEditorType.Item => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
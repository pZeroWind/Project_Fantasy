/*
 * 文件名：JsonSerializeAttribute.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/7
 * 
 * 文件描述：
 * 特性 用于标记可序列化json的类
 */

using Framework.Units;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Framework.Runtime
{
    public class JsonSerializeAttribute : Attribute
    {
        public JObject OnSerialize(object obj)
        {
            JObject json = new JObject();
            Queue<(object cur, JObject curJson)> queue = new Queue<(object, JObject)>();
            queue.Enqueue((obj, json));
            while (queue.Count > 0)
            {
                var (cur, curJson) = queue.Dequeue();
                var type = cur.GetType();
                var fields = JsonHelper.GetFieldInfoArr(type);
                foreach (var field in fields)
                {
                    var jField = field.GetCustomAttribute<JsonFieldAttribute>();
                    if (jField == null) continue;
                    var jName = jField.Name ?? field.Name;
                    var fVal = field.GetValue(cur);
                    switch (jField.DataType)
                    {
                        default:
                        case JsonType.String:
                            curJson.Add(jName, (string)fVal);
                            break;
                        case JsonType.Enum:
                            curJson.Add(jName, Enum.GetName(field.FieldType, fVal));
                            break;
                        case JsonType.Bool:
                            curJson.Add(jName, (bool)fVal);
                            break;
                        case JsonType.Int:
                            curJson.Add(jName, (int)fVal);
                            break;
                        case JsonType.Float:
                            curJson.Add(jName, (float)fVal);
                            break;
                        case JsonType.Double:
                            curJson.Add(jName, (double)fVal);
                            break;
                        case JsonType.Object:
                            {
                                if (fVal.GetType().GetCustomAttribute<JsonSerializeAttribute>() != null)
                                {
                                    var jsonObject = new JObject();
                                    curJson.Add(jName, jsonObject);
                                    queue.Enqueue((fVal, jsonObject));
                                }
                                else if (fVal is IDataProperty data)
                                {
                                    curJson.Add(jName, data.Serialize());
                                }
                            }
                            break;
                    }
                }
            }
            return json;
        }

        public void OnDeserialize(JObject json, object obj)
        {
            Queue<(object, JObject curJson)> queue = new Queue<(object, JObject)>();
            queue.Enqueue((obj, json));
            while (queue.Count > 0)
            {
                var (cur, curJson) = queue.Dequeue();
                var type = cur.GetType();
                var fields = JsonHelper.GetFieldInfoArr(type);
                foreach (var field in fields)
                {
                    var jField = field.GetCustomAttribute<JsonFieldAttribute>();
                    if (jField == null) continue;
                    var jName = jField.Name ?? field.Name;
                    switch (jField.DataType)
                    {
                        default:
                        case JsonType.String:
                            field.SetValue(cur, curJson[jName].Value<string>());
                            break;
                        case JsonType.Enum:
                            field.SetValue(cur, Enum.Parse(field.FieldType, curJson[jName].Value<string>()));
                            break;
                        case JsonType.Bool:
                            field.SetValue(cur, curJson[jName].Value<bool>());
                            break;
                        case JsonType.Int:
                            field.SetValue(cur, curJson[jName].Value<int>());
                            break;
                        case JsonType.Float:
                            field.SetValue(cur, curJson[jName].Value<float>());
                            break;
                        case JsonType.Double:
                            field.SetValue(cur, curJson[jName].Value<double>());
                            break;
                        case JsonType.Object:
                            {
                                var fieldObj = Activator.CreateInstance(field.FieldType);
                                if (fieldObj.GetType().GetCustomAttribute<JsonSerializeAttribute>() != null)
                                {
                                    field.SetValue(cur, fieldObj);
                                    queue.Enqueue((fieldObj, (JObject)curJson[jName]));
                                }
                                else if (fieldObj is IDataProperty data)
                                {
                                    data.Deserialize(curJson[jName]);
                                }
                            }
                            break;
                    }
                }
            }
        }

        
    }
}


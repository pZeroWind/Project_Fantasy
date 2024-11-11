/*
 * 文件名：JsonHelper.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/7
 * 
 * 文件描述：
 * 静态JSON工具类
 */

using Framework.Runtime;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Units
{
    public static class JsonHelper
    {
        public static JObject JsonSerialize(this object data)
        {
            var attr = data.GetType().GetCustomAttribute<JsonSerializableAttribute>();
            return attr?.OnSerialize(data);
        }

        public static void JsonDeserialize(this object data, JToken json)
        {
            if (json is JObject jsonObject)
            {
                var attr = data.GetType().GetCustomAttribute<JsonSerializableAttribute>();
                attr?.OnDeserialize(jsonObject, data);
            }
        }

        public static IEnumerable<FieldInfo> GetFieldInfoArr(Type type)
        {
            Stack<Type> stack = new Stack<Type>();
            stack.Push(type);
            var parentType = type.BaseType;
            while (parentType != null)
            {
                stack.Push(parentType);
                parentType = parentType.BaseType;
            }
            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                var list = cur.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var field in list)
                {
                    yield return field;
                }
            }
        }
    }
}


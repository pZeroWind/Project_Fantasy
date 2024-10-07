/*
 * 文件名：JsonHelper.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/7
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/7
 * 
 * 文件描述：
 * 静态JSON工具类
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Framework.Units
{
    public static class JsonHelper
    {
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


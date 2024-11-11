/*
 * 文件名：JsonFieldAttribute.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 特性 用于标记类中可以从Json数据中获取的字段
 */

using System;

namespace Framework.Runtime
{
    public enum JsonType
    {
        String,
        Enum,
        Bool,
        Int,
        Float,
        Double,
        Object,
        GameObject
    }

    public class JsonFieldAttribute : Attribute
    {
        public string Name { get; private set; }

        public JsonType DataType { get; private set; }

        public JsonFieldAttribute(string Name = null, JsonType DataType = JsonType.String)
        {
            this.Name = Name;
            this.DataType = DataType;
        }
    }
}


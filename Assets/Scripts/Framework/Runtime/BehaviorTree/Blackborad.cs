﻿/*
 * 文件名：LeafNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 行为树类
 */

using System.Collections.Generic;

namespace Framework.Runtime.Behavior
{
    public interface IProperty
    {
        
    }

    public class Property<T> : IProperty
    {
        public T Value;

        public Property() { }

        public Property(T value) 
        {
            Value = value;
        }

        public T GetValue()
        {
            return Value;
        }

        public void SetValue(T val)
        {
            Value = val;
        }
    }

    public class Blackborad
    {
        private readonly Dictionary<string, IProperty> _properties;

        public Blackborad()
        {
            _properties = new Dictionary<string, IProperty>();
        }

        public void Set<T>(string key, T value)
        {
            if (_properties.TryGetValue(key, out IProperty property) && property is Property<T> p)
            {
                p.SetValue(value);
            }
            _properties.Add(key, new Property<T>(value));
        }

        public void Remove(string key)
        {
            _properties.Remove(key);
        }

        public T Find<T>(string key)
        {
            if (_properties.TryGetValue(key, out IProperty property))
            {
                if (property is Property<T> p)
                    return p.Value;
            }
            return default;
        }
    }
}


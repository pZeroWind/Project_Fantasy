﻿/*
 * 文件名：Singleton.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 代表当前类为全局单例
 */

namespace Framework.Runtime
{
    public abstract class Singleton<T> : IGameManager where T : Singleton<T>, new()
    {
        private static T _value = null;

        private static readonly object _lock = new object();

        public virtual void OnInit() { }

        public static T Instance
        {
            get
            {
                if(_value == null)
                {
                    lock (_lock)
                    {
                        if (_value == null)
                        {
                            _value = new T();
                            _value.OnInit();
                        }
                    }
                }
                return _value;
            }
        }
    }
}


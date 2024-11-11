/*
 * 文件名：MonoSingleton.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 代表当前类为全局GameObject单例
 */

using UnityEngine;

namespace Framework.Runtime
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _value = null;

        private static readonly object _lock = new object();

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
                            var go = new GameObject();
                            go.AddComponent<T>();
                            DontDestroyOnLoad(go);
                        }
                    }
                }
                return _value;
            }
        }
    }
}


/*
 * 文件名：Singleton.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 代表当前类为全局单例
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


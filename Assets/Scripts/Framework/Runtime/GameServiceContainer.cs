/*
 * 文件名：GameServiceContainer.cs
 * 作者：ZeroWind
 * 创建时间：2024/9/30
 * 
 * 文件描述：
 * 服务容器类，用于管理游戏运行时注册的服务对象
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Framework.Runtime
{
    /// <summary>
    /// 注入类型
    /// </summary>
    public enum InjectType
    {
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 瞬时
        /// </summary>
        Transient,
        
        /// <summary>
        /// 定义域
        /// </summary>
        Scoped
    }

    public class GameServiceContainer
    {
        /// <summary>
        /// 单例对象创建函数字典
        /// </summary>
        private readonly Dictionary<string, Func<object>> _singletonServiceFuncs = new Dictionary<string, Func<object>>();

        /// <summary>
        /// 单例对象字典
        /// </summary>
        private readonly Dictionary<string, object> _singletonServices = new Dictionary<string, object>();

        /// <summary>
        /// 瞬时对象创建函数字典
        /// </summary>
        private readonly Dictionary<string, Func<object>> _transientServiceFuncs = new Dictionary<string, Func<object>>();

        /// <summary>
        /// 定义域对象创建函数字典
        /// </summary>
        private readonly Dictionary<string, Func<object>> _scopedServiceFuncs = new Dictionary<string, Func<object>>();

        /// <summary>
        /// 定义域对象字典
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, object>> _scopedServices = new Dictionary<string, Dictionary<string, object>>();

        /// <summary>
        /// 服务对象的注入类型字典
        /// </summary>
        private readonly Dictionary<string, InjectType> _injectTypeDict = new Dictionary<string, InjectType>();

        /// <summary>
        /// 已注入的对象
        /// </summary>
        private readonly List<object> _hasInjectObjects = new List<object>();

        #region 单例对象
        /// <summary>
        /// 注册单例对象
        /// </summary>
        public void AddSingleton<T>(string keyName = null, T value = null) where T : class, new()
        {
            keyName ??= typeof(T).Name;
            if (_injectTypeDict.ContainsKey(keyName)) return;
            if (value == null)
                _singletonServiceFuncs.Add(keyName, () => new T());
            else
                _singletonServiceFuncs.Add(keyName, () => value);
            _injectTypeDict.Add(keyName, InjectType.Singleton);
        }

        /// <summary>
        /// 获取单例对象
        /// </summary>
        public T GetSingleton<T>(string keyName) where T : class
        {
            if(_injectTypeDict.TryGetValue(keyName, out var injectType) &&
                injectType == InjectType.Singleton)
            {
                if(_singletonServices.TryGetValue(keyName, out var value))
                {
                    return value as T;
                }
                else if(_singletonServiceFuncs.TryGetValue(keyName, out var func))
                {
                    var res = func.Invoke();
                    _singletonServices.TryAdd(keyName, res);
                    return res as T;
                }
            }
            return null;
        }
        #endregion

        #region 瞬时对象
        /// <summary>
        /// 注册瞬时对象
        /// </summary>
        public void AddTransient<T>(string keyName = null, T value = null) where T : class, new()
        {
            keyName ??= typeof(T).Name;
            if (_injectTypeDict.ContainsKey(keyName)) return;
            if (value == null)
                _transientServiceFuncs.Add(keyName, () => new T());
            else
                _transientServiceFuncs.Add(keyName, () => value);
            _injectTypeDict.Add(keyName, InjectType.Transient);
        }

        /// <summary>
        /// 获取瞬时对象
        /// </summary>
        public T GetTransient<T>(string keyName) where T : class
        {
            if (_injectTypeDict.TryGetValue(keyName, out var injectType) &&
                injectType == InjectType.Transient)
            {
                if (_transientServiceFuncs.TryGetValue(keyName, out var func))
                {
                    var res = func.Invoke();
                    return res as T;
                }
            }
            return null;
        }
        #endregion

        #region 定义域对象
        /// <summary>
        /// 注册瞬时对象
        /// </summary>
        public void AddScoped<T>(string keyName = null, T value = null) where T : class, new()
        {
            keyName ??= typeof(T).Name;
            if (_injectTypeDict.ContainsKey(keyName)) return;
            if (value == null)
                _transientServiceFuncs.Add(keyName, () => new T());
            else
                _transientServiceFuncs.Add(keyName, () => value);
            _injectTypeDict.Add(keyName, InjectType.Scoped);
        }

        /// <summary>
        /// 获取瞬时对象
        /// </summary>
        public T GetScoped<T>(string keyName, string scopedName) where T : class
        {
            if (_injectTypeDict.TryGetValue(keyName, out var injectType) &&
                injectType == InjectType.Scoped)
            {
                if (_scopedServices.TryGetValue(scopedName, out var valueDict) &&
                    valueDict.TryGetValue(keyName, out var value))
                {
                    return value as T;
                }
                else if (_scopedServiceFuncs.TryGetValue(keyName, out var func))
                {
                    var res = func.Invoke();
                    if (!_scopedServices.TryGetValue(scopedName, out var dict))
                    {
                        dict = new Dictionary<string, object>();
                        _scopedServices.Add(scopedName, dict);
                    }
                    dict.Add(keyName, res);
                    return res as T;
                }
            }
            return null;
        }
        #endregion
        
        /// <summary>
        /// 加载服务到对象
        /// </summary>
        public void LoadService(object obj)
        {
            Queue<object> queue = new Queue<object>();
            queue.Enqueue(obj);
            while (queue.Count > 0)
            {
                var curObj = queue.Dequeue();
                Type type = curObj.GetType();
                // 反射获取包含特性的属性和字段
                var props = type.GetProperties
                    (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Select(p => new { Attr = p.GetCustomAttribute<InjectObjectAttribute>(), Value = p })
                    .Where(p => p.Attr != null)
                    .GetEnumerator();
                var fields = type.GetFields
                    (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Select(f => new { Attr = f.GetCustomAttribute<InjectObjectAttribute>(), Value = f })
                    .Where(f => f.Attr != null)
                    .GetEnumerator();
                // 遍历属性以及字段
                while (props.MoveNext())
                {
                    var p = props.Current;
                    string keyName = p.Attr.KeyName ?? p.Value.PropertyType.Name;
                    string scopedName = p.Attr.Scoped;
                    var value = GetService(keyName, scopedName);
                    p.Value.SetValue(obj, value, null);
                    if (IsService(value)) queue.Enqueue(value);
                }
                while (fields.MoveNext())
                {
                    var f = fields.Current;
                    string keyName = f.Attr.KeyName ?? f.Value.FieldType.Name;
                    string scopedName = f.Attr.Scoped;
                    var value = GetService(keyName, scopedName);
                    f.Value.SetValue(obj, value);
                    if (IsService(value)) queue.Enqueue(value);
                }
            }
        }

        private object GetService(string keyName, string scopedName)
        {
            if (!_injectTypeDict.TryGetValue(keyName, out var type)) return null;
            return type switch
            {
                InjectType.Singleton => GetSingleton<object>(keyName),
                InjectType.Transient => GetTransient<object>(keyName),
                InjectType.Scoped => GetScoped<object>(keyName, scopedName),
                _ => GetSingleton<object>(keyName),
            };
        }

        private bool IsService(object obj)
        {
            if (obj == null) return false;
            if (_hasInjectObjects.Contains(obj)) return false;
            Type type = obj.GetType();
            var attr = type.GetCustomAttribute<GameServiceAttribute>();
            var res = attr != null;
            if (res)
            {
                _hasInjectObjects.Add(obj);
            }
            return res;
        }
    }
}

/*
 * 文件名：UIManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * UI管理器 集中管理当前场景下所有被定义的UI
 */

using Framework.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Runtime.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private readonly Dictionary<string, UIDefiner> _uiDefinerById = new Dictionary<string, UIDefiner>();

        private readonly Dictionary<string, List<UIDefiner>> _uiDefinerByClass = new Dictionary<string, List<UIDefiner>>();

        /// <summary>
        /// 添加UI定义器
        /// </summary>
        public void AddUIDefiner(UIDefiner definer)
        {
            if (!string.IsNullOrEmpty(definer.Id))
            {
                if (_uiDefinerById.ContainsKey(definer.Id))
                {
                    GameLogManager.Instance.Error($"添加UIDefiner时错误 ID:[{definer.Id}]已被定义");
                    return;
                }
                _uiDefinerById[definer.Id] = definer;
            }

            if (!string.IsNullOrEmpty(definer.Class))
            {
                if (!_uiDefinerByClass.TryGetValue(definer.Class, out var list))
                {
                    list = new List<UIDefiner>();
                    _uiDefinerByClass[definer.Class] = list;
                }
                list.Add(definer);
            }
        }

        /// <summary>
        /// 移除UI定义器
        /// </summary>
        public void RemoveUIDefiner(UIDefiner definer)
        {
            if (!string.IsNullOrEmpty(definer.Id))
                _uiDefinerById.Remove(definer.Id);

            if (!string.IsNullOrEmpty(definer.Class) && _uiDefinerByClass.TryGetValue(definer.Class, out var list))
            {
                list.Remove(definer);
                if (list.Count == 0) _uiDefinerByClass.Remove(definer.Class);
            }
        }

        /// <summary>
        /// 按Id查找UI
        /// </summary>
        public T FindById<T>(string id) where T : Object
        {
            return _uiDefinerById.TryGetValue(id, out var definer) && definer.GetComponent<T>() is T result ?
                result : null;
        }

        /// <summary>
        /// 按Class查找UI
        /// </summary>
        public IEnumerable<T> FindByClass<T>(string className) where T : Object
        {
            if (_uiDefinerByClass.TryGetValue(className, out var definers))
            {
                foreach (var definer in definers)
                {
                    if (definer.TryGetComponent<T>(out var result))
                        yield return result;
                }
            }
        }
    }
}


/*
 * 文件名：UIManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * UI管理器 集中管理当前场景下所有被定义的UI
 */

using System.Collections.Generic;

namespace Framework.Runtime.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private readonly Dictionary<string, UIViewModel> _uiVModelById = new Dictionary<string, UIViewModel>();

        private readonly Dictionary<string, List<UIViewModel>> _uiVModelByClass = new Dictionary<string, List<UIViewModel>>();

        /// <summary>
        /// 添加UI定义器
        /// </summary>
        public void AddUIDefiner<T>(UIDefiner<T> definer) where T : UIViewModel, new()
        {
            if (!string.IsNullOrEmpty(definer.Id))
            {
                if (_uiVModelById.ContainsKey(definer.Id))
                {
                    GameLogManager.Instance.Error($"添加UIDefiner时错误 ID:[{definer.Id}]已被定义");
                    return;
                }
                _uiVModelById.Add(definer.Id, definer.ViewModel);
            }

            if (!string.IsNullOrEmpty(definer.Class))
            {
                if (!_uiVModelByClass.TryGetValue(definer.Class, out var list))
                {
                    list = new List<UIViewModel>();
                    _uiVModelByClass[definer.Class] = list;
                }
                list.Add(definer.ViewModel);
            }
        }

        /// <summary>
        /// 移除UI定义器
        /// </summary>
        public void RemoveUIDefiner<T>(UIDefiner<T> definer) where T : UIViewModel, new()
        {
            if (!string.IsNullOrEmpty(definer.Id))
                _uiVModelById.Remove(definer.Id);

            if (!string.IsNullOrEmpty(definer.Class) && _uiVModelByClass.TryGetValue(definer.Class, out var list))
            {
                list.Remove(definer.ViewModel);
                if (list.Count == 0) _uiVModelByClass.Remove(definer.Class);
            }
        }

        /// <summary>
        /// 按Id查找UI
        /// </summary>
        public T FindById<T>(string id) where T : UIViewModel
        {
            return _uiVModelById.TryGetValue(id, out var vm) && vm is T result ?
                result : null;
        }

        /// <summary>
        /// 按Class查找UI
        /// </summary>
        public IEnumerable<T> FindByClass<T>(string className) where T : UIViewModel
        {
            if (_uiVModelByClass.TryGetValue(className, out var vms))
            {
                foreach (var vm in vms)
                {
                    if (vm is T result)
                        yield return result;
                }
            }
        }
    }
}


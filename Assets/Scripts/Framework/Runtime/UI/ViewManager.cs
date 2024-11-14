/*
 * 文件名：ViewManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * 视图管理器 集中管理当前场景下所有被定义的视图
 */

using System.Collections.Generic;

namespace Framework.Runtime.UI
{
    public class ViewManager : Singleton<ViewManager>
    {
        private readonly Dictionary<string, IViewPresenter> _uiVModelById = new Dictionary<string, IViewPresenter>();

        private readonly Dictionary<string, List<IViewPresenter>> _uiVModelByClass = new Dictionary<string, List<IViewPresenter>>();

        /// <summary>
        /// 添加定义器
        /// </summary>
        public void AddDefiner<T>(ViewDefiner<T> definer) where T : IViewPresenter, new()
        {
            if (!string.IsNullOrEmpty(definer.Id))
            {
                if (_uiVModelById.ContainsKey(definer.Id))
                {
                    GameLogManager.Instance.Error($"添加UIDefiner时错误 ID:[{definer.Id}]已被定义");
                    return;
                }
                _uiVModelById.Add(definer.Id, definer.Presenter);
            }

            if (!string.IsNullOrEmpty(definer.Class))
            {
                if (!_uiVModelByClass.TryGetValue(definer.Class, out var list))
                {
                    list = new List<IViewPresenter>();
                    _uiVModelByClass[definer.Class] = list;
                }
                list.Add(definer.Presenter);
            }
        }

        /// <summary>
        /// 移除定义器
        /// </summary>
        public void RemoveDefiner<T>(ViewDefiner<T> definer) where T : IViewPresenter, new()
        {
            if (!string.IsNullOrEmpty(definer.Id))
                _uiVModelById.Remove(definer.Id);

            if (!string.IsNullOrEmpty(definer.Class) && _uiVModelByClass.TryGetValue(definer.Class, out var list))
            {
                list.Remove(definer.Presenter);
                if (list.Count == 0) _uiVModelByClass.Remove(definer.Class);
            }
        }

        /// <summary>
        /// 按Id查找
        /// </summary>
        public T FindById<T>(string id) where T : IViewPresenter
        {
            return _uiVModelById.TryGetValue(id, out var vm) && vm is T result ?
                result : default;
        }

        /// <summary>
        /// 按Class查找UI
        /// </summary>
        public IEnumerable<T> FindByClass<T>(string className) where T : IViewPresenter
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


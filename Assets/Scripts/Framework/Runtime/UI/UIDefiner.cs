/*
 * 文件名：UIDefiner.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * UI定义器 定义某个GameObject为UI 需继承来实现其初始化内容
 */

using UnityEngine;
using static UnityEditor.Profiling.HierarchyFrameDataView;

namespace Framework.Runtime.UI
{
    public abstract class UIDefiner<T> : MonoBehaviour where T : UIViewModel, new()
    {
        public string Id;

        public string Class;

        public T ViewModel;

        protected abstract void OnInitialize();

        private void Start()
        {
            ViewModel = new T();
            ViewModel.BindView(gameObject);
            UIManager.Instance.AddUIDefiner(this);
            OnInitialize();
        }

        private void OnDestroy()
        {
            UIManager.Instance.RemoveUIDefiner(this);
        }
    }
}


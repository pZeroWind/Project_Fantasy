/*
 * 文件名：ViewDefiner.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * 视图定义器 定义某个GameObject为视图 需继承来实现其初始化内容
 */

using UnityEngine;

namespace Framework.Runtime.UI
{
    public abstract class ViewDefiner<T> : MonoBehaviour where T : IViewPresenter, new()
    {
        public string Id;

        public string Class;

        public T Presenter;

        protected abstract void OnInitialize();

        private void Start()
        {
            Presenter = new T();
            //初始化表示器
            Presenter.OnInitalize(gameObject);
            ViewManager.Instance.AddDefiner(this);
            OnInitialize();
            //渲染表示器
            Presenter.OnRender();
        }

        private void OnDestroy()
        {
            ViewManager.Instance.RemoveDefiner(this);
        }
    }
}

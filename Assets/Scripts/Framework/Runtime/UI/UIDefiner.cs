/*
 * 文件名：UIDefiner.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * UI定义器 定义某个GameObject为UI 需继承来实现其初始化内容
 */

using UnityEngine;

namespace Framework.Runtime.UI
{
    public abstract class UIDefiner : MonoBehaviour
    {
        public string Id = string.Empty;

        public string Class = string.Empty;

        protected abstract void OnInitialize();

        private void Start()
        {
            UIManager.Instance.AddUIDefiner(this);
            OnInitialize();
        }

        private void OnDestroy()
        {
            UIManager.Instance.RemoveUIDefiner(this);
        }
    }
}


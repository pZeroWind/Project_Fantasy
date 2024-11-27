/*
 * 文件名：IViewPresenter.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/15
 * 
 * 文件描述：
 * 视图表示器接口 实现后用于处理业务逻辑
 */

using UnityEngine;

namespace Framework.Runtime.UI
{
    public interface IViewPresenter
    {
        void OnInitalize(GameObject view);

        void OnRender();
    }
}


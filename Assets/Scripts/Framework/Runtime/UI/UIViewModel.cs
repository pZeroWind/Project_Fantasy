/*
 * 文件名：UIViewModel.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/14
 * 
 * 文件描述：
 * UI视图模型 用于定义该UI可以操控的属性
 */

using UnityEngine;

namespace Framework.Runtime.UI
{
    public abstract class UIViewModel
    {
        public abstract void BindView(GameObject view);
    }
}


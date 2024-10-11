/*
 * 文件名：AnimationHelper.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/11
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/11
 * 
 * 文件描述：
 * 静态动画工具类
 */

using UnityEngine;

namespace Framework.Units
{
    public static class AnimationHelper
    {
        public static void SetAnimation(this Animator animator, string name, float corossTime = 0f)
        {
            animator.CrossFadeInFixedTime(name, corossTime);
        }
    }
}

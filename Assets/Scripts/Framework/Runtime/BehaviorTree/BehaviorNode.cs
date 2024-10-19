/*
 * 文件名：BehaviorNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/18
 * 
 * 文件描述：
 * 行为节点基类
 */

using System.Collections.Generic;

namespace Framework.Runtime.Behavior
{
    public enum NodeResult
    {
        Success,
        Failure,
        Runing
    }

    public abstract class BehaviorNode
    {

        public abstract NodeResult OnExecute(Blackborad blackborad, float fTick);
    }
}


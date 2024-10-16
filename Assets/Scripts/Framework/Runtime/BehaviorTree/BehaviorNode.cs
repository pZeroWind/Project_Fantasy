/*
 * 文件名：BehaviorNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
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
        private readonly List<BehaviorNode> _children;

        public void AddChildren(params BehaviorNode[] nodes)
        {
            _children.AddRange(nodes);
        }

        public BehaviorNode()
        {
            _children = new List<BehaviorNode>();
        }

        public abstract NodeResult OnExecute(Blackborad blackborad, float fTick);
    }
}


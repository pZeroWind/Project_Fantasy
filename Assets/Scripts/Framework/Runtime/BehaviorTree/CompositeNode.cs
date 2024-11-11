/*
 * 文件名：CompositeNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/14
 * 
 * 文件描述：
 * 组合节点基类
 */

using System.Collections.Generic;

namespace Framework.Runtime.Behavior
{
    public abstract class CompositeNode : BehaviorNode
    {
        private readonly List<BehaviorNode> _children;

        protected IEnumerator<BehaviorNode> GetChildren() => _children.GetEnumerator();

        public void AddChildren(params BehaviorNode[] nodes)
        {
            _children.AddRange(nodes);
        }

        public CompositeNode()
        {
            _children = new List<BehaviorNode>();
        }
    }
}


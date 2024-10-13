/*
 * 文件名：BehaviorTree.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/13
 * 
 * 文件描述：
 * 行为树类
 */

using System.Collections.Generic;

namespace Framework.Runtime.Behavior
{
    public class BehaviorTree
    {
        private BehaviorNode _root;

        public void SetRoot(BehaviorNode root) => _root = root;
    }

    public abstract class BehaviorNode
    {
        private readonly List<BehaviorNode> _children;

        public List<BehaviorNode> Children => _children;

        public void AddChildren(params BehaviorNode[] nodes)
        {
            _children.AddRange(nodes);
        }

        public BehaviorNode()
        {
            _children = new List<BehaviorNode>();
        }
    }

    public class LeafNode : BehaviorNode
    {

    }

    public class CompositeNode : BehaviorNode
    {

    }
}


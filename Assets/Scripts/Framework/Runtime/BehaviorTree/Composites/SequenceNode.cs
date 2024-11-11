/*
 * 文件名：SequenceNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/18
 * 
 * 文件描述：
 * 顺序节点
 */

namespace Framework.Runtime.Behavior.Composites
{
    public class SequenceNode : CompositeNode
    {
        public override NodeResult OnExecute(Blackborad blackborad, float fTick)
        {
            // 遍历所有子节点
            using var children = GetChildren();
            while (children.MoveNext())
            {
                var child = children.Current;
                var result = child.OnExecute(blackborad, fTick);
                // 若为失败或运行中 返回当前结果
                if (result is NodeResult.Failure or NodeResult.Runing) return result;
            }
            // 否则返回成功
            return NodeResult.Success;
        }
    }
}



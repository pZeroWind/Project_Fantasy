/*
 * 文件名：ParallelNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/18
 * 
 * 文件描述：
 * 并行节点
 */

namespace Framework.Runtime.Behavior.Composites
{
    public class ParallelNode : CompositeNode
    {
        public override NodeResult OnExecute(Blackborad blackborad, float fTick)
        {
            bool fail = false;
            bool running = false;
            // 遍历所有子节点
            using var children = GetChildren();
            while (children.MoveNext())
            {
                var child = children.Current;
                var result = child.OnExecute(blackborad, fTick);
                if(result == NodeResult.Failure)
                    fail = true;
                if(result == NodeResult.Runing)
                    running = true;
            }
            // 返回结果
            return running ? NodeResult.Runing : 
                (fail ? NodeResult.Failure : NodeResult.Success);
        }
    }
}


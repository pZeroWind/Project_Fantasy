/*
 * 文件名：ConditionNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/18
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/18
 * 
 * 文件描述：
 * 行动节点
 */
namespace Framework.Runtime.Behavior.Leafs
{
    public abstract class ConditionNode : LeafNode
    {
        public abstract bool OnCondition(Blackborad blackborad, float fTick);

        public override NodeResult OnExecute(Blackborad blackborad, float fTick)
        {
            return OnCondition(blackborad, fTick) ? NodeResult.Success : NodeResult.Failure;
        }
    }
}

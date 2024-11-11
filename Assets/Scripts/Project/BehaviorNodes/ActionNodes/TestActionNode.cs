/*
 * 文件名：TestActionNode.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/21
 * 
 * 文件描述：
 * 行为树测试节点
 */

using Framework.Runtime.Behavior;
using Framework.Runtime.Behavior.Leafs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Behavior
{
    public class TestActionNode : ActionNode
    {
        public override NodeResult OnExecute(Blackborad blackborad, float fTick)
        {
            return NodeResult.Success;
        }
    }
}

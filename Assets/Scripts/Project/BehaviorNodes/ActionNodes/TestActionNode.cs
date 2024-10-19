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

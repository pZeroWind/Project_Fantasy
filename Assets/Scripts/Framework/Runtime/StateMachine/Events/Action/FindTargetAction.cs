/*
 * 文件名：FindTargetAction.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/10
 * 
 * 文件描述：
 * 状态机寻找目标行为事件
 */

using System.Linq;

namespace Framework.Runtime.States
{
    public class FindTargetAction : ActionEvent
    {
        public override ActionEvent Clone()
        {
            return new FindTargetAction
            {
                Duration = Duration,
                Condition = Condition.Select(c => c.Clone()).ToList(),
                Time = Time,
            };
        }

        protected override void OnAction(Entity entity, float fTick)
        {
            //if (entity.Is<CharacterEntity>(out var character))
            //{
            //    var target = character.transform.right * Direction;
            //    character.Controller.Move(target);
            //}
        }
    }
}


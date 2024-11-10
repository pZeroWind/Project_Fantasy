using UnityEngine;

namespace Framework.Runtime.States
{
    public class InputMoveCondition : ConditionEvent
    {
        protected override bool OnCondition(Entity entity)
        {
            if(entity.Is<PlayerEntity>(out var player))
            {
                return player.Move != Vector3.zero;
            }
            return false;
        }

        public override ConditionEvent Clone()
        {
            return new InputMoveCondition
            {
                Invert = Invert,
            };
        }
    }
}


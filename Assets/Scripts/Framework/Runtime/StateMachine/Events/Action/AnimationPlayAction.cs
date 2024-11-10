using Framework.Units;
using System.Linq;
using UnityEngine;

namespace Framework.Runtime.States
{
    public class AnimationPlayAction : ActionEvent
    {
        public string Name;

        protected override void OnAction(Entity entity, float fTick)
        {
            if (entity.Is<CharacterEntity>(out var character))
            {
                character.Animator.SetAnimation(Name, 0.1f);
            }
        }

        public override ActionEvent Clone()
        {
            return new AnimationPlayAction
            {
                Name = Name,
                Duration = Duration,
                Time = Time,
                Condition = Condition.Select(c => c.Clone()).ToList(),
            };
        }
    }
}


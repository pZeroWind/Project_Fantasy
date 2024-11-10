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


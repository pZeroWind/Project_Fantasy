using System.Linq;

namespace Framework.Runtime.States
{
    public class MoveAction : ActionEvent
    {
        protected override void OnAction(Entity entity, float fTick)
        {
            if (entity.Is<CharacterEntity>(out var character) && character.Data.Is<CharacterEntityData>(out var data))
            {
                var target = (data.PropertyData.Speed / 10) * fTick * character.Move;
                character.Controller.Move(target);
            }
        }

        public override ActionEvent Clone()
        {
            return new MoveAction()
            {
                Condition = Condition.Select(c => c.Clone()).ToList(),
                Duration = Duration,
                Time = Time,
            };
        }
    }
}


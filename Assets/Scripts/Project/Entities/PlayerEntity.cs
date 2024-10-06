/*
 * 文件名：Entity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 玩家实体运行时
 */

using Framework.Runtime;
using Newtonsoft.Json.Linq;
using Project.States;
using Unity.VisualScripting;

namespace Project.Entities
{
    [GameEntity("Prefabs/Player")]
    public class PlayerEntity : Entity
    {
        public CharacterEntityData EntityData = new CharacterEntityData();

        [InjectObject]
        public InputService InputService;

        public override void OnInit()
        {
            StateMachine.AddState<PlayerIdle>(StateType.Idle);
            StateMachine.AddState<PlayerMove>(StateType.Move);
        }

        public override void OnUpdate(float fTick)
        {
            InputService?.OnUpdate();
        }

        public override JObject Serialize()
        {
            return EntityData.Serialize();
        }

        public override void Deserialize(JObject json)
        {
            EntityData.Deserialize(json);
        }
    }
}


/*
 * 文件名：PlayerMove.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 玩家移动状态
 */

using Framework.Runtime;
using Project.Entities;
using UnityEngine;

namespace Project.States
{
    public class PlayerMove : State
    {
        public override void OnInit(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                AddCanToState(StateType.Idle, () => player.InputService.Move == Vector3.zero);
            }
        }

        public override void OnExecuteState(Entity entity, float fTick)
        {
            if (entity is PlayerEntity player)
            {
                player.transform.Translate
                    ((player.EntityData.PropertyData.Speed / 10f) * 
                    fTick * 
                    player.InputService.Move);
            }
        }
    }
}

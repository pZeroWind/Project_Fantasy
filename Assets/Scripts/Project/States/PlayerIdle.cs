/*
 * 文件名：PlayerIdle.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 玩家待机状态
 */

using Framework.Runtime;
using Framework.Units;
using Project.Entities;
using UnityEngine;

namespace Project.States
{
    public class PlayerIdle : State
    {
        public override void OnInit(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                AddCanToState(StateType.Move, () => player.InputService.Move != Vector3.zero);
            }
        }

        public override void OnEnterState(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                player.Animator.SetAnimation(StateType.Idle.ToString());
            }
        }
    }
}


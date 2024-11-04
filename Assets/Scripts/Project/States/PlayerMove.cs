/*
 * 文件名：PlayerMove.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/5
 * 
 * 文件描述：
 * 玩家移动状态
 */

using Framework.Runtime;
using Framework.Units;
using Project.Entities;
using System.Linq;
using UnityEngine;

namespace Project.States
{
    public class PlayerMove : State
    {
        private float _rotate = 0;

        private float _x;

        private float _z;

        public override void OnInit(Entity entity)
        {
            if (entity.Is<PlayerEntity>(out var player))
            {
                AddCanToState(StateType.Idle, () => player.InputService.Move == Vector3.zero);
            }
        }

        public override void OnEnterState(Entity entity)
        {
            if (entity.Is<PlayerEntity>(out var player))
            {
                player.Animator.SetAnimation(StateType.Move.ToString());
            }
        }

        public override void OnExecuteState(Entity entity, float fTick)
        {
            if (entity.Is<PlayerEntity>(out var player))
            {
                // 计算旋转角度
                _x = player.transform.rotation.eulerAngles.x;
                _z = player.transform.rotation.eulerAngles.z;
                float speed = player.Data.As<CharacterEntityData>().PropertyData.Speed / 10;
                Vector3 movePosition = player.InputService.Move;
                player.Controller.Move(fTick * speed * movePosition);
                if (player.InputService.Move.x != 0)
                {
                    _rotate = player.InputService.Move.x > 0 ? 180 : 0;
                    _x = player.InputService.Move.x > 0 ? -_x : _x;
                }
                player.transform.rotation = Quaternion.Euler(_x, _rotate, _z);
            }
        }
    }
}

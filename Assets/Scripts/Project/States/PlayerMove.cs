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
using Framework.Units;
using Project.Entities;
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
            if (entity is PlayerEntity player)
            {
                AddCanToState(StateType.Idle, () => player.InputService.Move == Vector3.zero);
            }
        }

        public override void OnEnterState(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                player.Animator.SetAnimation(StateType.Move.ToString());
            }
        }

        public override void OnExecuteState(Entity entity, float fTick)
        {
            if (entity is PlayerEntity player)
            {
                // 计算旋转角度以及移动方向
                player.CharacterController.Move(fTick 
                    * (player.GetData<CharacterEntityData>().PropertyData.Speed / 10f) 
                    * player.InputService.Move);
                _x = player.transform.rotation.eulerAngles.x;
                _z = player.transform.rotation.eulerAngles.z;
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

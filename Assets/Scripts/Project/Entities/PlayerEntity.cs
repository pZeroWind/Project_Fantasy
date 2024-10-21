/*
 * 文件名：Entity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/18
 * 
 * 文件描述：
 * 玩家实体运行时
 */

using Framework.Runtime;
using Project.States;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Entities
{
    [GameEntity("Prefabs/Characters/Player")]
    public class PlayerEntity : CharacterEntity
    {
        [InjectObject]
        public InputService InputService;

        public Animator Animator;

        public Rigidbody2D RigidBody;

        public override void OnStart()
        {
            Animator = GetComponent<Animator>();
            RigidBody = GetComponent<Rigidbody2D>();
            StateMachine.AddState<PlayerIdle>(StateType.Idle);
            StateMachine.AddState<PlayerMove>(StateType.Move);
        }

        public override void OnUpdate(float fTick)
        {
            InputService?.OnUpdate();
        }
    }
}


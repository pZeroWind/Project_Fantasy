/*
 * 文件名：Entity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/4
 * 
 * 文件描述：
 * 玩家实体运行时
 */

using UnityEngine;

namespace Framework.Runtime
{
    public class PlayerEntity : CharacterEntity
    {
        [InjectObject]
        public InputService InputService;

        public override void OnStart()
        {
            Animator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();
        }

        public override void OnUpdate(float fTick)
        {
            base.OnUpdate(fTick);
            if (InputService != null)
            {
                InputService.OnUpdate(this);
            }
        }
    }
}


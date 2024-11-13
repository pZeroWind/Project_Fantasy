/*
 * 文件名：Entity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 玩家实体运行时
 */

using Framework.Runtime.UI;
using Project.UI.ViewModel;
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
            UIManager.Instance.FindById<ValueBarViewModel>("HpBar").Value = 0;
        }

        public override void OnUpdate(float fTick)
        {
            base.OnUpdate(fTick);
            if (InputService != null)
            {
                InputService.OnUpdate(this);
            }
            UIManager.Instance.FindById<ValueBarViewModel>("HpBar").Value += fTick / 10f; 
        }
    }
}


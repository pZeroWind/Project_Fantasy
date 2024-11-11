/*
 * 文件名：ActionEvent.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/9
 * 
 * 文件描述：
 * 状态机行为事件抽象类
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Runtime.States
{
    public abstract class ActionEvent : IEvent, ICloneable<ActionEvent>
    {
        public float Time;

        public float Duration;

        public List<ConditionEvent> Condition = new List<ConditionEvent>();

        private bool _trigger = false;

        protected abstract void OnAction(Entity entity, float fTick);

        public virtual void OnEnter(Entity entity)
        {
            _trigger = false;
        }

        public virtual void OnExit(Entity entity) 
        {
            
        }

        public bool OnExecute(Entity entity, float fTick, float lifeTime)
        {
            
            if (!_trigger && Time <= lifeTime && Condition.All(condition => condition.OnExecute(entity, fTick, lifeTime)))
            {
                OnAction(entity, fTick);
                if (lifeTime >= Duration)
                    _trigger = true;
            }
            return true;
        }

        public abstract ActionEvent Clone();
    }
}

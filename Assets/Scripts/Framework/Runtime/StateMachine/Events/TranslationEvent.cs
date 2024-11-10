/*
 * 文件名：TranslationEvent.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/9
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/9
 * 
 * 文件描述：
 * 状态机切换状态事件抽象类
 */

using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework.Runtime.States
{
    public class TranslationEvent : IEvent ,ICloneable<TranslationEvent>
    {
        public float Time;

        public string ToState;

        public List<ConditionEvent> Condition = new List<ConditionEvent>();

        public virtual void OnEnter(Entity entity)
        {
        }

        public bool OnExecute(Entity entity,  float fTick, float lifeTime)
        {
            if (lifeTime < Time) return false;
            return Condition.All(condition => condition.OnExecute(entity, fTick, lifeTime));
        }

        public virtual void OnExit(Entity entity)
        {
            
        }



        public TranslationEvent Clone()
        {
            return new TranslationEvent
            {
                Time = Time,
                ToState = ToState,
                Condition = Condition.Select(c => c.Clone()).ToList()
            };
        }
    }
}

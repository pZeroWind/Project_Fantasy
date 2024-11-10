/*
 * 文件名：ConditionEvents.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/9
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/9
 * 
 * 文件描述：
 * 状态机条件事件抽象类
 */

namespace Framework.Runtime.States
{
    public abstract class ConditionEvent : IEvent, ICloneable<ConditionEvent>
    {
        public bool Invert;

        protected abstract bool OnCondition(Entity entity);

        public bool OnExecute(Entity entity, float fTick, float lifeTime)
        {
            bool result = OnCondition(entity);
            return Invert ? (!result) : result;
        }

        public virtual void OnEnter(Entity entity)
        { 
        }

        public virtual void OnExit(Entity entity)
        {
        }

        public abstract ConditionEvent Clone();
    }
}
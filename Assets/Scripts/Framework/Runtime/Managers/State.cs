/*
 * 文件名：State.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/19
 * 
 * 文件描述：
 * 实体状态抽象类
 */

using System;
using System.Collections.Generic;

namespace Framework.Runtime
{
    public abstract class State
    {
        private readonly List<(StateType Type, Func<bool> Condition)> _canToStates;

        public State() 
        {
            _canToStates = new List<(StateType Type, Func<bool> Condition)>();
        }

        public void AddCanToState(StateType type, Func<bool> condition)
        {
            _canToStates.Add((type, condition));
        }

        public IEnumerator<(StateType Type, Func<bool> Condition)> GetCanToState()
        {
            return _canToStates.GetEnumerator();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void OnInit(Entity entity);

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void OnEnterState(Entity entity) { }

        /// <summary>
        /// 执行状态
        /// </summary>
        public virtual void OnExecuteState(Entity entity, float fTick) { }

        /// <summary>
        /// 执行状态
        /// </summary>
        public virtual void OnExecuteFixedState(Entity entity, float fiexdTick) { }

        /// <summary>
        /// 退出状态
        /// </summary>
        public virtual void OnExitState(Entity entity) { }
    }
}


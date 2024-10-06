/*
 * 文件名：StateMachine.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 实体状态机类
 */

using System;
using System.Collections.Generic;

namespace Framework.Runtime
{
    public enum StateType
    {
        Idle,
        Move,
        Attack
    }

    [Serializable]
    public class StateMachine
    {
        private readonly Dictionary<StateType, State> _states = new Dictionary<StateType, State>();

        public List<State> States = new List<State>();

        public StateType CurrentState = StateType.Idle;

        private readonly object _currentStateLock = new object();

        private readonly Entity _owner;

        public StateMachine(Entity owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// 向状态机添加状态
        /// </summary>
        public void AddState<T>(StateType type) where T : State, new()
        {
            if (!_states.ContainsKey(type))
            {
                var state = new T();
                state.OnInit(_owner);
                _states.Add(type, state);
                States.Add(state);
            }
        }

        /// <summary>
        /// 执行状态机
        /// </summary>
        public void OnUpdate(Entity entity, float fTick)
        {
            lock (_currentStateLock)
            {
                if (_states.ContainsKey(CurrentState))
                {
                    var canToStates = _states[CurrentState].CanToStates;
                    foreach (var canToState in canToStates)
                    {
                        bool condition = canToState.Condition.Invoke();
                        if (condition)
                        {
                            _states[CurrentState].OnExitState(entity);
                            CurrentState = canToState.Type;
                            _states[CurrentState].OnEnterState(entity);
                        }
                    }
                    _states[CurrentState].OnExecuteState(entity, fTick);
                }
            }
        }
    }
}


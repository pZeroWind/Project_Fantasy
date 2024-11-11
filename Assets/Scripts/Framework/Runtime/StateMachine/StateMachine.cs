/*
 * 文件名：StateMachine.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 实体状态机类
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Runtime.States
{

    [Serializable]
    public class StateMachine
    {
        private readonly Dictionary<string, State> _states = new Dictionary<string, State>();

        private string _currentState = string.Empty;

        private bool _run = false;

        public List<State> States = new List<State>();

        public string CurrentState => _currentState;

        public void OnLoad(string name, string defaultState) 
        {
            States.AddRange(StateMachineManager.Instance.LoadStateMachine(name));
            foreach (var state in States)
            {
                _states.Add(state.StateName, state);
            }
            _currentState = defaultState;
            _run = true;
        }

        /// <summary>
        /// 执行状态机
        /// </summary>
        public void OnUpdate(Entity entity, float fTick)
        {
            if (_run && _states.ContainsKey(_currentState))
            {
                var (success, name) = _states[_currentState].OnExecuteTranslation(entity, fTick);
                if (success)
                {
                    _states[_currentState].OnExitState(entity);
                    _currentState = name;
                    _states[_currentState].OnEnterState(entity);
                    return;
                }
                _states[_currentState].OnExecuteState(entity, fTick);
            }
        }
    }
}


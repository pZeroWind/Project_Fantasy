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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Runtime.States
{
    public class State : ICloneable<State>
    {
        public string StateName = string.Empty;

        public List<ActionEvent> ActionEvents = new List<ActionEvent>();

        public List<TranslationEvent> TranslationEvents = new List<TranslationEvent>();

        private float _lifeTime = 0f;

        public State Clone()
        {
            return new State
            {
                StateName = StateName,
                ActionEvents = ActionEvents.Select(ac => ac.Clone()).ToList(),
                TranslationEvents = TranslationEvents.Select(trans => trans.Clone()).ToList()
            };
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public void OnEnterState(Entity entity) 
        {
            _lifeTime = 0f;
            foreach (var action in ActionEvents)
            {
                action.OnEnter(entity);
            }
            foreach (var translation in TranslationEvents)
            {
                translation.OnEnter(entity);
            }
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public void OnExecuteState(Entity entity, float fTick)
        {
            _lifeTime += fTick;
            foreach (var action in ActionEvents)
            {
                action.OnExecute(entity, fTick, _lifeTime);
            }
        }

        public (bool success, string name) OnExecuteTranslation(Entity entity, float fTick)
        {
            foreach (var translation in TranslationEvents)
            {
                bool success = translation.OnExecute(entity, fTick, _lifeTime);
                if (success)
                {
                    return (success, translation.ToState);
                }
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// 退出状态
        /// </summary>
        public void OnExitState(Entity entity)
        {
            foreach (var action in ActionEvents)
            {
                action.OnExit(entity);
            }
            foreach (var translation in TranslationEvents)
            {
                translation.OnExit(entity);
            }
        }
    }
}


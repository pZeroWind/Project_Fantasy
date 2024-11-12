/*
 * 文件名：Entity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 角色实体运行时抽象类
 */

using Framework.Runtime.States;
using Framework.Units;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

namespace Framework.Runtime
{
    public abstract class Entity : MonoBehaviour, IConvertible<Entity>
    {
        public EntityData Data;

        public BuffManager BuffMgr;

        public EntityManager EntityMgr;

        public StateMachine StateMachine;

        [Range(0f, 1f)]
        public float TimeScale = 1.0f;

        IEnumerator Start()
        {
            StateMachine = new StateMachine();
            yield return new WaitUntil(() => BuffMgr != null && EntityMgr != null && StateMachine != null);
            StateMachine.OnLoad(Data.StateMachine, Data.DefaultState);
            OnStart();
        }

        void Update()
        {
            OnUpdate(Time.deltaTime * TimeScale);
            StateMachine.OnUpdate(this, Time.deltaTime * TimeScale);
            BuffMgr.OnUpdate(Time.deltaTime * TimeScale);
        }

        void FixedUpdate()
        {
            OnFixedUpdate(Time.fixedDeltaTime * TimeScale);
        }

        void LateUpdate()
        {
            OnLateUpdate(Time.deltaTime * TimeScale);
        }

        public virtual void OnStart() { }

        public virtual void OnUpdate(float fTick) { }

        public virtual void OnFixedUpdate(float fTick) { }

        public virtual void OnLateUpdate(float fTick) { }

        public virtual JObject Serialize()
        {
            return Data.JsonSerialize();
        }

        public virtual void Deserialize(EntityData entity)
        {
            Data = entity;
        }

        public S As<S>() where S : Entity
        {
            return this as S;
        }

        public bool Is<S>(out S result) where S : Entity
        {
            if (this is S res)
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }
    }
}


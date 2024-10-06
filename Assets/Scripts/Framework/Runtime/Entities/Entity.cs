/*
 * 文件名：Entity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 角色实体运行时抽象类
 */

using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

namespace Framework.Runtime
{
    public abstract class Entity : MonoBehaviour
    {
        public BuffManager BuffMgr;

        public EntityManager EntityMgr;

        public StateMachine StateMachine;

        [Range(0f, 1f)]
        public float TimeScale = 1.0f;

        IEnumerator Start()
        {
            BuffMgr = new BuffManager();
            StateMachine = new StateMachine(this);
            yield return new WaitUntil(() => BuffMgr != null && EntityMgr != null && StateMachine != null);
            OnInit();
        }

        void Update()
        {
            OnUpdate(Time.deltaTime * TimeScale);
            StateMachine.OnUpdate(this, Time.deltaTime * TimeScale);
        }

        void FixedUpdate()
        {
            OnFixedUpdate(Time.fixedDeltaTime * TimeScale);    
        }

        void LateUpdate()
        {
            OnLateUpdate(Time.deltaTime * TimeScale);
            BuffMgr.OnUpdate(Time.deltaTime * TimeScale);
        }

        public virtual void OnInit() { }

        public virtual void OnUpdate(float fTick) { }

        public virtual void OnFixedUpdate(float fTick) { }

        public virtual void OnLateUpdate(float fTick) { }

        public abstract JObject Serialize();

        public abstract void Deserialize(JObject json);
    }
}


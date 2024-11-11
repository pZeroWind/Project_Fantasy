/*
 * 文件名：EventManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/7
 * 
 * 文件描述：
 * 游戏运行时的事件管理器
 */
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Framework.Runtime
{
    public class Effect : MonoBehaviour
    {
        public string EffectName;

        public ParticleSystem Particle;

        public void OnInit(string effectName)
        {
            EffectName = effectName;
            Particle = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (Particle != null && Particle.isStopped)
            {
                EffectManager.Instance.RecycleEffect(EffectName, Particle);
            }
        }
    }

    public class EffectManager : MonoSingleton<EffectManager>, IGameManager
    {
        private float _idleTime = 5f;
        
        private readonly Dictionary<string, Queue<ParticleSystem>> _particleQueueHash = new Dictionary<string, Queue<ParticleSystem>>();
        private readonly Dictionary<string, GameObject> _prefabHash = new Dictionary<string, GameObject>();
        private readonly ConcurrentDictionary<ParticleSystem, float> _poolHash = new ConcurrentDictionary<ParticleSystem, float>();
        public void OnInit()
        {
            var list = GameResourceManager.Instance.LoadAll<GameObject>("Prefabs/Effects");
            foreach (var particle in list)
            {
                _prefabHash.Add(particle.name, particle);
                _particleQueueHash.Add(particle.name, new Queue<ParticleSystem>());
            }
        }

        void Update()
        {
            Stack<ParticleSystem> romoveStack = new Stack<ParticleSystem>();
            using (var enumerator = _poolHash.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var cur = enumerator.Current;
                    if(!cur.Key.gameObject.activeSelf && cur.Value >= _idleTime)
                    {
                        romoveStack.Push(cur.Key);
                        continue;
                    }
                    _poolHash[cur.Key] += Time.deltaTime;
                }
            }
            while (romoveStack.Count > 0)
            {
                var remove = romoveStack.Pop();
                if (_poolHash.TryRemove(remove, out _))
                {
                    GameObject.DestroyImmediate(remove.gameObject);
                }
            }
        }

        /// <summary>
        /// 获取特效
        /// </summary>
        public ParticleSystem GetEffect(string name)
        {
            if(_particleQueueHash.TryGetValue(name, out var particles) && _prefabHash.TryGetValue(name, out var prefab))
            {
                while (particles.Count > 0)
                {
                    var particle = particles.Dequeue();
                    if (particle != null)
                    {
                        _poolHash.TryRemove(particle, out _);
                        particle.gameObject.SetActive(true);
                        return particle;
                    }
                }
                var go = GameObject.Instantiate(prefab);
                if (go != null && go.TryGetComponent<ParticleSystem>(out var newParticle))
                {
                    var effect = newParticle.AddComponent<Effect>();
                    effect.OnInit(name);
                    return newParticle;
                }
            }
            return null;
        }

        /// <summary>
        /// 播放特效
        /// </summary>
        public void PlayEffect(string name, Vector3 position)
        {
            var p = GetEffect(name);
            if (p != null)
            {
                p.transform.position = position;
                p.Play();
            }
        }

        //public void PlayEffectWithFollow(string name, Transform transform)
        //{
        //    var p = GetEffect(name);
        //    if (p != null)
        //    {
        //        p.gameObject.
        //        p.Play();
        //    }
        //}

        /// <summary>
        /// 归还特效
        /// </summary>
        public void RecycleEffect(string name, ParticleSystem particle)
        {
            if (_particleQueueHash.TryGetValue(name, out var particles))
            {
                particle.gameObject.SetActive(false);
                particles.Enqueue(particle);
                _poolHash.TryAdd(particle, 0f);
            }
        }
    }
}


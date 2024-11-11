/*
 * 文件名：EntityManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 管理当前程序中所有的实体
 */

using Framework.Units;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Framework.Runtime
{
    public enum EntityType
    {
        /// <summary>
        /// 玩家
        /// </summary>
        Player,

        /// <summary>
        /// 敌对
        /// </summary>
        Hostile,

        /// <summary>
        /// 友好
        /// </summary>
        Friendly,

        /// <summary>
        /// 中立
        /// </summary>
        Neutral,

        /// <summary>
        /// 可破坏物
        /// </summary>
        Destructible
    }

    public class EntityManager : IGameManager
    {
        private readonly Dictionary<EntityType, Dictionary<string, List<Entity>>> _entities = 
            new Dictionary<EntityType, Dictionary<string, List<Entity>>>();

        private readonly Dictionary<string, JObject> _entityJson = new Dictionary<string, JObject>();

        private GameServiceContainer _services;

        public void OnInit(GameServiceContainer services)
        {
            _services = services;
            OnInit();
        }

        public void OnInit()
        {
            if (_services == null) throw new System.NullReferenceException();
            var textArr = GameResourceManager.Instance.LoadAll<TextAsset>("Data/EntityData");
            foreach (var txt in textArr)
            {
                var json = JObject.Parse(txt.text);
                _entityJson.Add(json["EntityId"].Value<string>(), json);
            }
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        public T AddEntity<T, S>(EntityType type, string entityId, Vector3 pos, Quaternion rotate)
            where T : Entity, new()
            where S : EntityData, new()
        {
            //var entityType = typeof(T);
            //var attr = entityType.GetCustomAttribute<GameEntityAttribute>();
            //if (attr == null)
            //{
            //    Debug.LogWarning($"This entity is not GameEnitiy");
            //    return null;
            //}
            if (!_entityJson.TryGetValue(entityId, out var json))
            {
                Debug.LogWarning($"This entity does not have JSON");
                return null;
            }
            S data = new S();
            data.JsonDeserialize(json);
            GameObject prefab = GameResourceManager.Instance.Load<GameObject>(data.EntityPrefab);
            if (prefab == null) return null;
            GameObject go = GameObject.Instantiate(prefab, pos, rotate == Quaternion.identity ? prefab.transform.rotation : rotate);
            T entity = go.AddComponent<T>();
            entity.EntityMgr = this;
            entity.Deserialize(data);
            if (!_entities.TryGetValue(type, out var dict))
            {
                dict = new Dictionary<string, List<Entity>>();
                _entities.Add(type, dict);
            }
            if (!dict.TryGetValue(entityId, out var list))
            {
                list = new List<Entity>();
                dict.Add(entityId, list);
            }
            list.Add(entity);
            var layer = GameObject.Find("EntityLayer");
            if (layer != null)
            {
                go.transform.SetParent(layer.transform);
            }
            _services.LoadService(entity);
            return entity;
        }
    }
}


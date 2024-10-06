/*
 * 文件名：EntityManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 管理当前程序中所有的实体
 */

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

    public class EntityManager
    {
        private readonly Dictionary<EntityType, Dictionary<string, List<Entity>>> _entities = 
            new Dictionary<EntityType, Dictionary<string, List<Entity>>>();

        private readonly Dictionary<string, JObject> _entityJson = new Dictionary<string, JObject>();

        private GameServiceContainer _services;

        public void OnInit(GameServiceContainer services)
        {
            _services = services;
            var textArr = Resources.LoadAll<TextAsset>("Data/EntityData");
            foreach (var txt in textArr)
            {
                var json = JObject.Parse(txt.text);
                _entityJson.Add(json["EntityId"].Value<string>(), json);
            }
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        public T AddEntity<T>(EntityType type, string entityId, Vector3 pos, Quaternion rotate)
            where T : Entity
        {
            var entityType = typeof(T);
            var attr = entityType.GetCustomAttribute<GameEntityAttribute>();
            if (attr == null)
            {
                Debug.LogWarning($"This entity is not GameEnitiy");
                return null;
            }
            if (!_entityJson.TryGetValue(entityId, out var json))
            {
                Debug.LogWarning($"This entity does not have JSON");
                return null;
            }
            T entity = GameObject.Instantiate(Resources.Load<T>(attr.ResPath), pos, rotate);
            entity.EntityMgr = this;
            entity.Deserialize(json);
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
            _services.LoadService(entity);
            return entity;
        }
    }
}


/*
 * 文件名：EntityData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/7
 * 
 * 文件描述：
 * 存储实体基本数据的类
 */

using System;

namespace Framework.Runtime
{
    public enum EntityDataType
    {
        [ETypeBinding(typeof(EntityData))]
        Default,

        [ETypeBinding(typeof(CharacterEntityData))]
        Character
    }

    [JsonSerialize]
    [Serializable]
    public class EntityData : IDataProperty
    {
        [JsonField(nameof(EntityId), JsonType.String)]
        public string EntityId;

        [JsonField(nameof(Name), JsonType.String)]
        public string Name;

        [JsonField(nameof(EntityDataType), JsonType.Enum)]
        public EntityDataType EntityDataType;
    }
}


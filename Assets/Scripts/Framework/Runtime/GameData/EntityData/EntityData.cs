/*
 * 文件名：EntityData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/10
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

    [JsonSerializable]
    [Serializable]
    public class EntityData
    {
        [JsonField("实体编号", JsonType.String)]
        public string EntityId;

        [JsonField("实体名称", JsonType.String)]
        public string Name;

        [JsonField("实体类型", JsonType.Enum)]
        public EntityDataType EntityDataType;
    }
}


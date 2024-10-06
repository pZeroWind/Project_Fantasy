/*
 * 文件名：CharacterEntityData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 存储角色实体数据的类
 */

using System;

namespace Framework.Runtime
{
    [JsonSerialize]
    [Serializable]
    public class CharacterEntityData : EntityData
    {
        [JsonField(nameof(PropertyData), JsonType.Object)]
        public CharacterPropertyData PropertyData;
    }
}


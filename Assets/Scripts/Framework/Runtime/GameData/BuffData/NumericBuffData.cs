/*
 * 文件名：NumericBuffData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/12
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/12
 * 
 * 文件描述：
 * 基础Buff数据
 */

using System;

namespace Framework.Runtime
{
    [JsonSerializable]
    [Serializable]
    public class NumericBuffData : BuffData
    {
        [JsonField("Buff属性数据", JsonType.Object)]
        public CharacterProperty PropertyData;
    }
}


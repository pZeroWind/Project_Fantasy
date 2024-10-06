/*
 * 文件名：BuffData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 基础Buff数据
 */

using System;

namespace Framework.Runtime
{
    [JsonSerialize]
    [Serializable]
    public class BuffData : IDataProperty
    {
        [JsonField(nameof(BuffId), JsonType.String)]
        public string BuffId;

        [JsonField(nameof(Type), JsonType.Enum)]
        public BuffType Type;
    }
}


/*
 * 文件名：BuffData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/10
 * 
 * 文件描述：
 * 基础Buff数据
 */

using System;

namespace Framework.Runtime
{
    public enum BuffDataType
    {
        [ETypeBinding(typeof(BuffData))]
        None,

        [ETypeBinding(typeof(NumericBuffData))]
        NumericBuff,

        [ETypeBinding(typeof(BuffData))]
        DotBuff,

        [ETypeBinding(typeof(BuffData))]
        ModifyBuff
    }

    [JsonSerializable]
    [Serializable]
    public class BuffData
    {
        [JsonField("Buff编号", JsonType.String)]
        public string BuffId;

        [JsonField("Buff名称", JsonType.String)]
        public string Name;

        [JsonField("Buff类型", JsonType.Enum)]
        public BuffDataType Type;
    }
}


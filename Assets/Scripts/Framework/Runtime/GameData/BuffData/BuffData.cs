/*
 * 文件名：BuffData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
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
    public class BuffData : IConvertible<BuffData>
    {
        [JsonField("Buff编号", JsonType.String)]
        public string BuffId;

        [JsonField("Buff名称", JsonType.String)]
        public string Name;

        [JsonField("是否允许叠加")]
        public bool IsStackable;

        [JsonField("Buff类型", JsonType.Enum)]
        public BuffDataType Type;

        public S As<S>() where S : BuffData
        {
            return this as S;
        }

        public bool Is<S>(out S result) where S : BuffData
        {
            if (this is S res)
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }
    }
}


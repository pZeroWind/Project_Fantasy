/*
 * 文件名：NumericBuffProperty.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 存储数值Buff具体数据的类
 */
using System;

namespace Framework.Runtime
{
    [JsonSerializable]
    [Serializable]
    public class NumericBuffProperty
    {
        [JsonField("攻击力", JsonType.Float)]
        public float Atk;

        [JsonField("攻击力百分比", JsonType.Float)]
        public float AtkPercent;

        [JsonField("防御力", JsonType.Float)]
        public float Def;

        [JsonField("防御力百分比", JsonType.Float)]
        public float DefPercent;

        [JsonField("暴击率", JsonType.Float)]
        public float Critical;

        [JsonField("暴击伤害", JsonType.Float)]
        public float CriticalDamage;

        [JsonField("生命值", JsonType.Float)]
        public float Hp;

        [JsonField("生命值百分比", JsonType.Float)]
        public float HpPercent;

        [JsonField("法力值", JsonType.Float)]
        public float Mp;

        [JsonField("法力值百分比", JsonType.Float)]
        public float MpPercent;

        [JsonField("移动速度", JsonType.Float)]
        public float Speed;

        [JsonField("移动速度百分比", JsonType.Float)]
        public float SpeedPercent;
    }
}

/*
 * 文件名：CharacterPropertyData.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/10
 * 
 * 文件描述：
 * 存储角色实体数据的类
 */

using System;

namespace Framework.Runtime
{
    [JsonSerializable]
    [Serializable]
    public class CharacterPropertyData
    {
        /// <summary>
        /// 攻击力
        /// </summary>
        [JsonField("攻击力", JsonType.Float)]
        public float Atk;

        /// <summary>
        /// 成长攻击力
        /// </summary>
        [JsonField("成长攻击力", JsonType.Float)]
        public float GrowAtk;

        /// <summary>
        /// 防御力
        /// </summary>
        [JsonField("防御力", JsonType.Float)]
        public float Def;

        /// <summary>
        /// 成长防御力
        /// </summary>
        [JsonField("成长防御力", JsonType.Float)]
        public float GrowDef;

        /// <summary>
        /// 暴击率
        /// </summary>
        [JsonField("暴击率", JsonType.Float)]
        public float Critical;

        /// <summary>
        /// 暴击伤害
        /// </summary>
        [JsonField("暴击伤害", JsonType.Float)]
        public float CriticalDamage;

        /// <summary>
        /// 生命值
        /// </summary>
        [JsonField("生命值", JsonType.Float)]
        public float Hp;

        /// <summary>
        /// 成长生命值
        /// </summary>
        [JsonField("成长生命值", JsonType.Float)]
        public float GrowHp;

        /// <summary>
        /// 法力值
        /// </summary>
        [JsonField("法力值", JsonType.Float)]
        public float Mp;

        /// <summary>
        /// 成长法力值
        /// </summary>
        [JsonField("成长法力值", JsonType.Float)]
        public float GrowMp;

        /// <summary>
        /// 速度
        /// </summary>
        [JsonField("移动速度", JsonType.Float)]
        public float Speed;
    }
}


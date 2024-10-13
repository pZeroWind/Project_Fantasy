/*
 * 文件名：Buff.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/12
 * 
 * 文件描述：
 * 基础Buff类 无效果
 */

using Framework.Units;
using Newtonsoft.Json.Linq;

namespace Framework.Runtime
{
    public class Buff
    {
        public BuffData BuffData;

        public Entity Caster;

        public Entity Target;

        public int Layer = 1;

        public static T Create<T>(BuffDataType type, Entity caster, Entity target)
            where T : Buff, new()
        {
            var buff = new T
            {
                Caster = caster,
                Target = target,
                BuffData = type switch
                {
                    BuffDataType.None => new BuffData(),
                    BuffDataType.NumericBuff => new NumericBuffData(),
                    BuffDataType.DotBuff => new BuffData(),
                    BuffDataType.ModifyBuff => new BuffData(),
                    _ => new BuffData(),
                }
            };
            return buff;
        }


        public virtual void OnApply()
        {

        }

        public virtual void OnUpdate(float fTick)
        {

        }

        public virtual void OnDelete()
        {

        }

        public virtual JObject Serialize()
        {
            return BuffData.JsonSerialize();
        }

        public virtual void Deserialize(JObject json)
        {
            BuffData.JsonDeserialize(json);
        }
    }
}



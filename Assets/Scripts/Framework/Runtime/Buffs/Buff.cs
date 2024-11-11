/*
 * 文件名：Buff.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 文件描述：
 * 基础Buff类 无效果
 */

using Framework.Units;
using Newtonsoft.Json.Linq;

namespace Framework.Runtime
{
    public interface IBuff 
    {
        
    }

    public class Buff
    {
        private int _layer = 1;

        public BuffData BuffData;

        public Entity Caster;

        public Entity Target;

        public int Layer
        {
            get => _layer;
            set
            {
                if (BuffData.IsStackable) 
                    _layer = value;
            }
        }

        public Buff(Entity caster, Entity target)
        {
            Caster = caster;
            Target = target;
        }

        public static Buff Create(BuffDataType type, Entity caster, Entity target)
        {
            Buff buff = null;
            switch (type)
            {
                default:
                case BuffDataType.None:
                    buff = new Buff(caster, target);
                    break;
                case BuffDataType.NumericBuff:
                    buff = new NumericBuff(caster, caster);
                    break;
                case BuffDataType.DotBuff:
                    break;
                case BuffDataType.ModifyBuff:
                    break;
            }
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



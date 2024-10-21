/*
 * 文件名：NumericBuff.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/19
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/19
 * 
 * 文件描述：
 * 数值Buff类 只提供数值提升
 */

using Framework.Units;
using Newtonsoft.Json.Linq;

namespace Framework.Runtime
{
    public class NumericBuff : Buff
    {
        public NumericBuff(Entity caster, Entity target) : base(caster, target)
        {
        }
    }
}



/*
 * 文件名：IDataProperty.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/2
 * 
 * 文件描述：
 * 代表当前类为数据属性
 */

using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Framework.Runtime
{
    public interface IDataProperty { }

    public static class DataPropertyStatic
    {
        public static JObject Serialize(this IDataProperty data)
        {
            var attr = data.GetType().GetCustomAttribute<JsonSerializeAttribute>();
            return attr?.OnSerialize(data);
        }

        public static void Deserialize(this IDataProperty data, JToken json)
        {
            if (json is JObject jsonObject)
            {
                var attr = data.GetType().GetCustomAttribute<JsonSerializeAttribute>();
                attr?.OnDeserialize(jsonObject, data);
            }
        }
    }
}


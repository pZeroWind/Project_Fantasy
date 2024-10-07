/*
 * 文件名：JsonFieldAttribute.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/2
 * 
 * 文件描述：
 * 特性 用于标记类中可以从Json数据中获取的字段
 */

using System;

namespace Framework.Runtime
{

    public class ETypeBindingAttribute : Attribute
    {
        public Type BindingType { get; set; }

        public ETypeBindingAttribute(Type type)
        {
            BindingType = type;
        }
    }
}


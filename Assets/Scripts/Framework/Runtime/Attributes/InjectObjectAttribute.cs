/*
 * 文件名：InjectObjectAttribute.cs
 * 作者：ZeroWind
 * 创建时间：2024/9/30
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/9/30
 * 
 * 文件描述：
 * 特性 用于标记类中需要通过容器注入的字属性或字段
 */

using System;


namespace Framework.Runtime
{
    public class InjectObjectAttribute : Attribute
    {
        public string KeyName { get; set; }

        public string Scoped { get; set; }

        public InjectObjectAttribute(string keyName = null ,string scoped = null)
        {
            if (keyName != null) KeyName = keyName;
            if (scoped != null) Scoped = scoped;
        }
    }
}

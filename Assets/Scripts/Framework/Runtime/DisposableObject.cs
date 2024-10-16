/*
 * 文件名：DisposableObject.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/17
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 非托管资源抽象类
 */

using System;

namespace Framework.Runtime
{
    public abstract class DisposableObject : IDisposable
    {
        public abstract void Dispose();
    }
}

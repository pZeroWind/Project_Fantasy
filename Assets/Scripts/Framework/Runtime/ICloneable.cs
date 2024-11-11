/*
 * 文件名：ICloneable.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/7
 * 
 * 文件描述：
 * 用于提供克隆对象方法的接口
 */

public interface ICloneable<T>
{
    T Clone();
}

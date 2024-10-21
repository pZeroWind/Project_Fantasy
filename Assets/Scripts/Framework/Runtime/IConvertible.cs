/*
 * 文件名：IConvertible.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/19
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/19
 * 
 * 文件描述：
 * 用于提供数值强制转换方法的接口
 */


public interface IConvertible<T>
{
    bool Is<S>(out S result) where S : T;

    S As<S>() where S : T;
}

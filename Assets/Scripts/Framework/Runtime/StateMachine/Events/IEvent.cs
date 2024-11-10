/*
 * 文件名：IEvent.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/9
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/9
 * 
 * 文件描述：
 * 状态机事件接口
 */

namespace Framework.Runtime.States
{
    public interface IEvent
    {
        void OnEnter(Entity entity);
        bool OnExecute(Entity entity, float fTick, float lifeTime);
        void OnExit(Entity entity);
    }
}

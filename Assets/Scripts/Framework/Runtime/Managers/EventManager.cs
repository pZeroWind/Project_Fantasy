/*
 * 文件名：EventManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/7
 * 
 * 文件描述：
 * 游戏运行时的事件管理器
 */
using System.Collections.Generic;


namespace Framework.Runtime
{
    public enum EventType
    {
        /// <summary>
        /// 音乐
        /// </summary>
        Music,

        /// <summary>
        /// 音效
        /// </summary>
        Fx,

        /// <summary>
        /// 特效
        /// </summary>
        Effect
    }

    public class EventManager : IGameManager
    {
        private readonly Dictionary<string, GameEvent> _gameEvents = new Dictionary<string, GameEvent>();
        
        public void OnInit()
        {

        }
    }
}


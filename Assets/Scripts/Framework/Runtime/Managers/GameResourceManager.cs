/*
 * 文件名：GameResourceManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/10
 * 
 * 文件描述：
 * 游戏资源管理器类
 */

using UnityEngine;

namespace Framework.Runtime
{
    public class GameResourceManager:Singleton<GameResourceManager>
    {
        public T[] LoadAll<T>(string path) where T : UnityEngine.Object
        {
            return Resources.LoadAll<T>(path);
        }


        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }
    }
}

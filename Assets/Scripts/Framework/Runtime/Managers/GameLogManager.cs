/*
 * 文件名：GameLogManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/10
 * 
 * 文件描述：
 * 游戏日志管理器类
 */

using System;
using UnityEngine;

namespace Framework.Runtime
{
    public class GameLogManager:Singleton<GameLogManager>
    {
        public void Info(string message)
        {
            Debug.Log($"INFO[{DateTime.UtcNow:g}]: {message}");
        }


        public void Error(string message)
        {
            Debug.LogError($"ERROR[{DateTime.UtcNow:g}]: {message}");
        }

        public void Warning(string message)
        {
            Debug.LogWarning($"WARNING[{DateTime.UtcNow:g}]: {message}");
        }
    }
}

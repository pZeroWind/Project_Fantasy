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

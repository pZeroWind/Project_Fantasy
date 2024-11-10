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

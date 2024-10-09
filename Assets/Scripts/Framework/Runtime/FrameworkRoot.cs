/*
 * 文件名：FrameworkRoot.cs
 * 作者：ZeroWind
 * 创建时间：2024/9/30
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 框架运行时最主要的部分，保证游戏正确运行的核心运行组件
 */

using System.Collections;
using UnityEngine;

namespace Framework.Runtime
{
    public abstract class FrameworkRoot : MonoBehaviour
    {
        private GameServiceContainer _serviceContaniner;

        private EntityManager _entityManager;

        private IEnumerator Start()
        {
            _serviceContaniner = new GameServiceContainer();
            _entityManager = new EntityManager();
            yield return new WaitUntil(() => _serviceContaniner != null && _entityManager != null);
            _entityManager.OnInit(_serviceContaniner);
            BuffManager.OnInit();
            OnInitialize(_serviceContaniner);
            OnMounted(_entityManager);
        }

        private void OnDestroy()
        {
            
        }

        /// <summary>
        /// 框架内容初始化
        /// </summary>
        public abstract void OnInitialize(GameServiceContainer service);

        /// <summary>
        /// 框架内容初始化完毕
        /// </summary>
        /// <returns></returns>
        public abstract void OnMounted(EntityManager entities);
    }
}


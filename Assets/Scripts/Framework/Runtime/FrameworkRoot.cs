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

using Framework.Runtime.Behavior;
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
            GameLogManager.Instance.Info($"游戏服务容器初始化完毕");
            _entityManager = new EntityManager();
            yield return new WaitUntil(() => _serviceContaniner != null && _entityManager != null);
            _entityManager.OnInit(_serviceContaniner);
            GameLogManager.Instance.Info($"实体管理器初始化完毕");
            BuffManager.OnInit();
            GameLogManager.Instance.Info($"BUFF管理器全局缓存加载完毕");
            OnInitialize(_serviceContaniner);
            GameLogManager.Instance.Info($"游戏框架初始化完毕");
            OnMounted(_entityManager);
            GameLogManager.Instance.Info($"场景内容加载完毕");
        }

        private void OnDestroy()
        {
            
        }

        /// <summary>
        /// 框架内容初始化
        /// </summary>
        public abstract void OnInitialize(GameServiceContainer service);

        /// <summary>
        /// 场景内容加载
        /// </summary>
        /// <returns></returns>
        public abstract void OnMounted(EntityManager entities);
    }
}


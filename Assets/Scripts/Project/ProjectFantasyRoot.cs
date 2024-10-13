/*
 * 文件名：ProjectFantasyRoot.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/13
 * 
 * 文件描述：
 * 项目的启动根节点
 */

using Framework.Runtime;
using Framework.Runtime.Behavior;
using Project.Entities;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class ProjectFantasyRoot : FrameworkRoot
    {
        public CameraCtrl CameraCtrl;
        private EntityManager _entities;

        public override void OnInitialize(GameServiceContainer service)
        {
            service.AddSingleton<InputService>();
        }

        public override void OnMounted(EntityManager entities)
        {
            _entities = entities;
            AsyncOperation gameScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            AsyncOperation worldScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            StartCoroutine(OnLoaded(gameScene));
        }

        private IEnumerator OnLoaded(AsyncOperation operation)
        {
            while (!operation.isDone)
            {
                yield return 0;
            }
            var player = _entities.AddEntity<PlayerEntity>(EntityType.Player, "1000", Vector3.zero, Quaternion.identity);
            CameraCtrl.BindEntity(player);
        }
    }
}

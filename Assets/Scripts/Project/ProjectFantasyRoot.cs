/*
 * 文件名：ProjectFantasyRoot.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 项目的启动根节点
 */

using Framework.Runtime;
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
            StartCoroutine(OnLoaded());
        }

        private IEnumerator OnLoaded()
        {
            AsyncOperation gameScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            yield return new WaitUntil(() => gameScene.isDone);
            var battle = GameObject.Instantiate(GameResourceManager.Instance.Load<GameObject>("Prefabs/UI/Pages/BattlePage"));
            var uiRoot = GameObject.Find("UIRoot");
            battle.transform.parent = uiRoot.transform;
            AsyncOperation worldScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            yield return new WaitUntil(() => worldScene.isDone);
            var sql = SqliteManager.Instance;
            var player = _entities.AddEntity<PlayerEntity, CharacterEntityData>(EntityType.Player, "1000", Vector3.zero, Quaternion.identity);
            CameraCtrl.BindTraget(player.transform);
            CameraCtrl.SetEdge(new Vector3(15, 0, -4));
        }
    }
}

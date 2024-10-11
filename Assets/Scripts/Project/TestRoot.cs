/*
 * 文件名：TestRoot.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/8
 * 
 * 文件描述：
 * Test项目的启动根节点
 */

using Framework.Runtime;
using Project.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class TestRoot : FrameworkRoot
    {
        public override void OnInitialize(GameServiceContainer service)
        {
            service.AddSingleton<InputService>();
        }

        public override void OnMounted(EntityManager entities)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            var player = entities.AddEntity<PlayerEntity>(EntityType.Player, "1000", Vector3.zero, Quaternion.identity);
        }
    }
}

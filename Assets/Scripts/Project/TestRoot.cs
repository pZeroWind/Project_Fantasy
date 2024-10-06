/*
 * 文件名：TestRoot.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/2
 * 
 * 文件描述：
 * Test项目的启动根节点
 */

using Framework.Runtime;
using Project.Entities;
using System.Collections;
using UnityEngine;

namespace Project
{
    public class TestRoot : FrameworkRoot
    {
        public override IEnumerator OnInitialize(GameServiceContainer service)
        {
            service.AddSingleton<InputService>();
            yield return 0;
        }

        public override IEnumerator OnMounted(EntityManager entities)
        {
            var player = entities.AddEntity<PlayerEntity>(EntityType.Player, "1000", Vector3.zero, Quaternion.identity);
            yield return new WaitUntil(() => player != null);
        }
    }
}

/*
 * 文件名：InputService.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 文件描述：
 * 输入服务类
 */

using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Framework.Runtime
{
    [GameService]
    public class InputService
    {
        public void OnUpdate(CharacterEntity character)
        {
            OnMove(character);
        }

        /// <summary>
        /// 移动方法
        /// </summary>
        /// <param name="character"></param>
        private void OnMove(CharacterEntity character)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            var move = new Vector3(h, 0, v);
            move.Normalize();
            character.Move = move;
        }
    }
}


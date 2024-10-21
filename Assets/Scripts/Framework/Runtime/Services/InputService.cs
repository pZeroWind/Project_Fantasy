/*
 * 文件名：InputService.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/18
 * 
 * 文件描述：
 * 实体状态机类
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Runtime
{
    [GameService]
    public class InputService
    {
        private Vector2 _move = Vector2.zero;

        public Vector2 Move => _move;

        public void OnUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            var move = new Vector2(h, v);
            move.Normalize();
            _move = move;
        }
    }
}


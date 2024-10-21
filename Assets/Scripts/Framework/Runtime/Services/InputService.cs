/*
 * �ļ�����InputService.cs
 * ���ߣ�ZeroWind
 * ����ʱ�䣺2024/10/2
 * 
 * ���༭�ߣ�ZeroWind
 * ���༭ʱ�䣺2024/10/18
 * 
 * �ļ�������
 * ʵ��״̬����
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


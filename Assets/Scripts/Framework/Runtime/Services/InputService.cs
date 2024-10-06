using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Runtime
{
    [GameService]
    public class InputService
    {
        private Vector3 _move = Vector3.zero;

        public Vector3 Move => _move;

        public void OnUpdate()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            var move = new Vector3(x, y, 0);
            move.Normalize();
            _move = move;
        }
    }
}


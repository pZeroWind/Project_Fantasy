/*
 * 文件名：CameraCtrl.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/4
 * 
 * 文件描述：
 * 用于控制摄像机位置
 */

using System;
using UnityEngine;

namespace Project
{
    public class CameraCtrl : MonoBehaviour
    {
        public Transform Target;

        [Range(0f, 1f)]
        public float Damping = 0.1f;

        private Vector3 _velocity = Vector3.zero;

        public void BindTraget(Transform target)
        {
            Target = target;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector2 pos = transform.position;
            if (Target != null) 
            {
                pos = Vector3.SmoothDamp(pos, Target.transform.position, ref _velocity, Damping);
            }
            transform.position = pos;
        }
    }
}

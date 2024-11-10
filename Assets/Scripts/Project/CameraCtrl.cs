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

using Framework.Runtime;
using System;
using UnityEngine;

namespace Project
{
    public class CameraCtrl : MonoBehaviour
    {
        public Transform Target;

        [Range(0f, 1f)]
        public float Damping = 0.1f;

        public Vector3 Edge = Vector3.zero;

        public void BindTraget(Transform target)
        {
            Target = target;
        }

        public void SetEdge(Vector3 edge)
        {
            Edge = edge;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 pos = transform.position;
            
            if (Target != null) 
            {
                pos = Vector3.Lerp(pos, Target.transform.position, Damping);
            }

            if (Edge.z > pos.z)
            {
                pos.z = Edge.z;
            }

            if(Edge.x < pos.x)
            {
                pos.x = Edge.x;
            }

            if (-Edge.x > pos.x)
            {
                pos.x = -Edge.x;
            }

            transform.position = pos;
        }
    }
}

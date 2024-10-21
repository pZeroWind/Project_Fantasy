/*
 * 文件名：CameraCtrl.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/13
 * 
 * 文件描述：
 * 用于控制摄像机位置
 */

using Framework.Runtime;
using Project.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class CameraCtrl : MonoBehaviour
    {
        public Entity Owner;

        public Vector2 Edge;

        [Range(0f, 1f)]
        public float Damping = 0.1f;

        public void BindEntity(Entity entity)
        {
            Owner = entity;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 pos = transform.position;
            if (Owner != null) 
            {
                pos = Vector3.Lerp(pos, Owner.transform.position, Damping);
            }
            if (Edge != Vector2.zero)
            {
                if (pos.x > Edge.x)
                    pos.x = Edge.x;
                else if (pos.x < -Edge.x)
                    pos.x = -Edge.x;
                if (pos.y > Edge.y)
                    pos.y = Edge.y;
                else if (pos.y < -Edge.y)
                    pos.y = -Edge.y;
            }
            transform.position = pos;
        }

        private void OnDrawGizmos()
        {
            Func<Vector2> leftTop = () =>
            {
                Vector2 pos = Edge;
                pos.x = -pos.x;
                return pos;
            };

            Func<Vector2> rightTop = () =>
            {
                Vector2 pos = Edge;
                return pos;
            };

            Func<Vector2> leftBottom = () =>
            {
                Vector2 pos = Edge;
                pos.x = -pos.x;
                pos.y = -pos.y;
                return pos;
            };

            Func<Vector2> rightBottom = () =>
            {
                Vector2 pos = Edge;
                pos.y = -pos.y;
                return pos;
            };
            Gizmos.DrawLine(leftTop.Invoke(), rightTop.Invoke());
            Gizmos.DrawLine(rightTop.Invoke(), rightBottom.Invoke());
            Gizmos.DrawLine(rightBottom.Invoke(), leftBottom.Invoke());
            Gizmos.DrawLine(leftBottom.Invoke(), leftTop.Invoke());
        }
    }
}

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class CameraCtrl : MonoBehaviour
    {
        public Entity Owner;

        [Range(0f, 1f)]
        public float Damping = 0.25f;

        public void BindEntity(Entity entity)
        {
            Owner = entity;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (Owner != null) 
            {
                transform.position = Vector3.Lerp(transform.position, Owner.transform.position, Damping);
            }
        }
    }
}

﻿/*
 * 文件名：CharacterEntity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/3
 * 
 * 文件描述：
 * 存储角色实体数据的类
 */

using System;

namespace Framework.Runtime
{
    [JsonSerializable]
    [Serializable]
    public class CharacterEntityData : EntityData
    {
        [JsonField("角色属性数据", JsonType.Object)]
        public CharacterProperty PropertyData;
    }
}


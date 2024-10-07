/*
 * 文件名：BuffManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/2
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/3
 * 
 * 文件描述：
 * 管理当前角色实体的Buff对象
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Runtime
{
    public class BuffManager
    {
        private readonly List<Buff> _buffs = new List<Buff>();

        private static readonly Dictionary<string, JObject> _buffData = new Dictionary<string, JObject>();

        public static void OnInit()
        {
            var jsonTexts = Resources.LoadAll<TextAsset>($"Data/BuffData");
            foreach (var txt in jsonTexts)
            {
                var json = JObject.Parse(txt.text);
                _buffData.Add(json["BuffId"].ToString(), json);
            }
        }

        public void AddBuff(string buffId, BuffDataType buffType, Entity caster, Entity target)
        {
            if (!_buffData.TryGetValue(buffId, out var json))
            {
                Debug.LogError("This buff dose not exist");
                return;
            }
            var buff = _buffs.FirstOrDefault(b => b.BuffData.BuffId == buffId &&
                            b.BuffData.Type == buffType &&
                            b.Caster == caster &&
                            b.Target == target);
            if (buff == null)
            {
                buff = Buff.Create<Buff>(buffType, caster, target);
                buff.Deserialize(json);
                buff.OnApply();
            }
            else
            {
                buff.Layer += 1;
            }
        }

        public void OnUpdate(float fTick)
        {
            foreach (var buff in _buffs)
            {
                buff.OnUpdate(fTick);
            }
        }

        public void RemoveBuff(string buffId, Entity caster, Entity target)
        {
            var buff = _buffs.FirstOrDefault(b => b.BuffData.BuffId == buffId &&
                            b.Caster == caster &&
                            b.Target == target);
            buff?.OnDelete();
        }
    }
}


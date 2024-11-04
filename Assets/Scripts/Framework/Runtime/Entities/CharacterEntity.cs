/*
 * 文件名：CharacterEntity.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/19
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/11/5
 * 
 * 文件描述：
 * 角色实体运行时抽象类
 */

using Framework.Units;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

namespace Framework.Runtime
{
    public abstract class CharacterEntity : Entity
    {
        private uint _level = 1;

        private float _currentExp = 0f;

        private readonly float _baseExp = 100f;

        public uint Level => _level;

        public float CurrentExp
        {
            get => _currentExp;
            set
            {
                _currentExp += value;
                LevelUpHandle();
            }
        }

        private void LevelUpHandle()
        {
            var data = Data.As<CharacterEntityData>();
            if (data != null)
            {
                var levelUpExp = CalculateLevelUpExp(data);
                if (_currentExp >= levelUpExp)
                {
                    // 计算超出的经验值
                    var temp = _currentExp - levelUpExp;
                    _currentExp = temp > 0f ? temp : 0f;
                    // 等级上升
                    _level += 1;
                }
            }
        }

        private float CalculateLevelUpExp(CharacterEntityData data)
        {
            return data.PropertyData.GrowExp * Mathf.Log((_level - 1), data.PropertyData.MultiplyExp) + _baseExp;
        }
    }
}


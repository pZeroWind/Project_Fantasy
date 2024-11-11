/*
 * 文件名：InputMoveCondition.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/10
 * 
 * 文件描述：
 * 状态机判断是否输入移动信息条件
 */

using UnityEngine;

namespace Framework.Runtime.States
{
    public class InputMoveCondition : ConditionEvent
    {
        protected override bool OnCondition(Entity entity)
        {
            if(entity.Is<CharacterEntity>(out var character))
            {
                return character.Move != Vector3.zero;
            }
            return false;
        }

        public override ConditionEvent Clone()
        {
            return new InputMoveCondition
            {
                Invert = Invert,
            };
        }
    }
}


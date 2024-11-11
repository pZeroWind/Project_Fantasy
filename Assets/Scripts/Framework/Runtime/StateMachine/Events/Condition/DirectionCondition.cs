/*
 * 文件名：MoveAction.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/10
 * 
 * 文件描述：
 * 状态机移动行为事件
 */

using System.Linq;
using UnityEngine;

namespace Framework.Runtime.States
{
    public class DirectionCondition : ConditionEvent
    {
        public int Direction = 1;

        protected override bool OnCondition(Entity entity)
        {
            if (entity.Is<CharacterEntity>(out var character))
            {
                switch(Direction)
                {
                    case 1:
                        return character.Move.x > 0;
                    case -1:
                        return character.Move.x < 0;
                }
            }
            return false;
        }

        public override ConditionEvent Clone()
        {
            return new DirectionCondition
            {
                Direction = Direction,
                Invert = Invert,
            };
        }
    }
}


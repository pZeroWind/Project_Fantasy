/*
 * �ļ�����InputService.cs
 * ���ߣ�ZeroWind
 * ����ʱ�䣺2024/10/2
 * 
 * ���༭�ߣ�ZeroWind
 * ���༭ʱ�䣺2024/11/10
 * 
 * �ļ�������
 * ���������
 */

using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Framework.Runtime
{
    [GameService]
    public class InputService
    {
        public void OnUpdate(CharacterEntity character)
        {
            OnMove(character);
        }

        /// <summary>
        /// �ƶ�����
        /// </summary>
        /// <param name="character"></param>
        private void OnMove(CharacterEntity character)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            var move = new Vector3(h, 0, v);
            move.Normalize();
            character.Move = move;
        }
    }
}


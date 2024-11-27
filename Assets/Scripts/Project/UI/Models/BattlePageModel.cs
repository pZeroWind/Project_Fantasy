/*
 * 文件名：BattlePageModel.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/14
 * 
 * 文件描述：
 * 战斗UI页面 数据模型
 */


using UnityEngine;

namespace Project.UI.Models
{
    public class BattlePageModel
    {
        public string GetTextByName(string name)
        {
            return name;
        }

        public float GetValueRatio(float curValue, float maxValue)
        {
            return Mathf.Clamp(curValue / maxValue, 0f, 1f);
        }
    }
}

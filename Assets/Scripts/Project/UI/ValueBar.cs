/*
 * 文件名：ValueBar.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * 进度条式数值显示
 */

using Framework.Runtime.UI;
using TMPro;
using UnityEngine.UI;

namespace Project.UI
{
    public class ValueBar : UIDefiner
    {
        public TMP_Text Name;

        public Slider ValueSlider;

        protected override void OnInitialize()
        {
            //ValueSlider.On
        }

        public void ChangeValue(float value)
        {
            ValueSlider.value += value;
        }
    }
}


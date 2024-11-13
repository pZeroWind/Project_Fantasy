/*
 * 文件名：ValueBarViewModel.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/14
 * 
 * 文件描述：
 * 进度条式数值 视图模型
 */

using Framework.Runtime.UI;
using UnityEngine;

namespace Project.UI.ViewModel
{
    public class ValueBarViewModel : UIViewModel
    {
        private ValueBar _view;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => _view.Name.text;
            set => _view.Name.text = value;
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public float Value 
        {
            get => _view.ValueSlider.value;
            set => _view.ValueSlider.value = value;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            get => _view.ValueSlider.maxValue;
            set => _view.ValueSlider.maxValue = value;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            get => _view.ValueSlider.minValue;
            set => _view.ValueSlider.minValue = value;
        }

        public override void BindView(GameObject view)
        {
            _view = view.GetComponent<ValueBar>();
        }
    }
}

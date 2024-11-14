/*
 * 文件名：ValueBarPresenter.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/14
 * 
 * 文件描述：
 * 进度条式数值 表示器
 */

using Framework.Runtime.UI;
using Project.UI.Models;
using UnityEngine;

namespace Project.UI.Presenter
{
    public class ValueBarPresenter : IViewPresenter
    {
        private ValueBar _view;

        private ValueBarModel _model;

        public void SetLabelName(string name)
        {
            _model.Name = name;
            _view.Name.text = _model.Name;
        }

        public void ChangeValue(float curValue, float maxValue)
        {
            if(_model.TryCheckValue(curValue, maxValue, out float ratio))
            {
                _model.Value = ratio;
            }
            _view.ValueSlider.value = _model.Value;
        }

        public void InitPresenter(GameObject view)
        {
            _view = view.GetComponent<ValueBar>();
            _model = new ValueBarModel();
        }
    }
}

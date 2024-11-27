/*
 * 文件名：BattlePagePresenter.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/14
 * 
 * 文件描述：
 * 战斗UI页面 表示器
 */

using Framework.Runtime.UI;
using Project.UI.Models;
using UnityEngine;

namespace Project.UI.Presenter
{
    public class BattlePagePresenter : IViewPresenter
    {
        private BattlePage _view;

        private BattlePageModel _model;

        /// <summary>
        /// 更新生命值标签
        /// </summary>
        public void UpdateHpLabelName(string name)
        {
            _view.HpLabel.text = _model.GetTextByName(name);
        }

        /// <summary>
        /// 更新生命显示比率
        /// </summary>
        public void UpdateHpValue(float curValue, float maxValue)
        {
            _view.HpBar.value = _model.GetValueRatio(curValue, maxValue);
        }


        /// <summary>
        /// 更新魔力值标签
        /// </summary>
        public void UpdateMpLabelName(string name)
        {
            _view.MpLabel.text = _model.GetTextByName(name);
        }

        /// <summary>
        /// 更新魔力显示比率
        /// </summary>
        public void UpdateMpValue(float curValue, float maxValue)
        {
            _view.MpBar.value = _model.GetValueRatio(curValue, maxValue);
        }

        public void OnInitalize(GameObject view)
        {
            _view = view.GetComponent<BattlePage>();
            _model = new BattlePageModel();
        }

        public void OnRender()
        {
            //UpdateHpLabelName("Local_Hp");
            //UpdateMpLabelName("Local_Mp");
        }
    }
}

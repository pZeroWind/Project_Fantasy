/*
 * 文件名：BattlePage.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/13
 * 
 * 文件描述：
 * 战斗UI页面
 */

using Framework.Runtime.UI;
using Project.UI.Presenter;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Project.UI
{
    public class BattlePage : ViewDefiner<BattlePagePresenter>
    {
        public VisualElement Root;

        public Label HpLabel;

        public Label MpLabel;

        public ProgressBar HpBar;

        public ProgressBar MpBar;

        protected override void OnInitialize()
        {
            Root = transform.Find(nameof(Root)).GetComponent<UIDocument>().rootVisualElement;
            HpLabel = Root.Q<Label>("hp-label");
            HpBar = Root.Q<ProgressBar>("hp-bar");
            MpLabel = Root.Q<Label>("mp-label");
            MpBar = Root.Q<ProgressBar>("mp-bar");
        }
    }
}


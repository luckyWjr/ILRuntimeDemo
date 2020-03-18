using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    public class GoodsItemView : UIView
    {
        Text m_nameText;
        Button m_buyBtn;
        string m_goodsName;

        //因为在Activator.CreateInstance实例化的时候，ugui.transform的Type为RectTransform
        public GoodsItemView(GameObject go, RectTransform parent) : base(go, parent)
        {
        }

        public override void Init()
        {
            base.Init();

            m_buyBtn.onClick.AddListener(OnBuyBtnClicked);
        }

        protected override void GetChild()
        {
            base.GetChild();
            m_nameText = transform.Find("NameText").GetComponent<Text>();
            m_buyBtn = transform.Find("BuyButton").GetComponent<Button>();
        }

        public void Setting(string name)
        {
            m_goodsName = name;
            m_nameText.text = m_goodsName;
        }

        void OnBuyBtnClicked()
        {
            UIHelper.ShowDialogConfirmAndCancel("购买提示", $"是否要购买{m_goodsName}？", () =>
            {
                UIHelper.ShowDialogOnlyConfirm("购买提示", $"购买成功，恭喜你获得{m_goodsName}？");
            });
        }
    }
}
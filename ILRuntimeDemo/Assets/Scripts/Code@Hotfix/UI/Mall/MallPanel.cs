using Hotfix.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("MallPanel")]
    public class MallPanel : UIPanel
    {
        Button m_closeBtn;

        public MallPanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();
            m_closeBtn = transform.Find("CloseButton").GetComponent<Button>();
            m_closeBtn.onClick.AddListener(OnCloseBtnClick);
        }

        void OnCloseBtnClick()
        {
            UIPanelManager.Instance.HidePanel();
        }
    }
}
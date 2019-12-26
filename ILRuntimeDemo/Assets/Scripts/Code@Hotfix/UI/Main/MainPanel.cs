using Hotfix.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("MainPanel")]
    public class MainPanel : UIPanel
    {
        Text m_contentText;
        Button m_mallBtn;
        Button m_logoutBtn;

        public MainPanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();
            m_contentText = transform.Find("ContentText").GetComponent<Text>();
            m_mallBtn = transform.Find("MallButton").GetComponent<Button>();
            m_logoutBtn = transform.Find("LogoutButton").GetComponent<Button>();

            m_mallBtn.onClick.AddListener(OnMallBtnClick);
            m_logoutBtn.onClick.AddListener(OnLogoutBtnClick);
        }

        public override void Show()
        {
            base.Show();
            m_contentText.text = $"用户名:{data}";
        }

        void OnMallBtnClick()
        {
            UIPanelManager.Instance.ShowPanel<MallPanel>();
        }

        void OnLogoutBtnClick()
        {
            UIPanelManager.Instance.ShowPanel<LoginPanel>();
            UIPanelManager.Instance.DestroyPanel<MallPanel>();
            UIPanelManager.Instance.DestroyPanel<MainPanel>();
        }
    }
}
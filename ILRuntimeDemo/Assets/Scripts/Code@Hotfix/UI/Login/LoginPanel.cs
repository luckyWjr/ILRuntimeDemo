using Hotfix.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("LoginPanel")]
    public class LoginPanel : UIPanel
    {
        Button m_loginBtn;
        Text m_hintText;

        public LoginPanel(string url) : base(url)
        {
            Debug.Log("LoginPanel:" + url);
        }

        public override void Init()
        {
            base.Init();
            m_loginBtn = transform.Find("Button").GetComponent<Button>();
            m_hintText = transform.Find("Text").GetComponent<Text>();

            m_loginBtn.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            UIPanelManager.Instance.ShowPanel<MainPanel>();
        }
    }
}
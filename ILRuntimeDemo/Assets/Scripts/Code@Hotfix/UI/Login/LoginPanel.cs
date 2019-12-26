using Hotfix.Manager;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("LoginPanel")]
    public class LoginPanel : UIPanel
    {
        Button m_loginBtn;
        InputField m_userNameInput;

        public LoginPanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();
            m_loginBtn = transform.Find("LoginButton").GetComponent<Button>();
            m_userNameInput = transform.Find("UserNameInputField").GetComponent<InputField>();

            m_loginBtn.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            UIPanelManager.Instance.ShowPanel<MainPanel>(m_userNameInput.text);
        }
    }
}
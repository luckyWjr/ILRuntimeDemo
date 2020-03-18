using Hotfix.Manager;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("MainPanel")]
    public class MainPanel : UIPanel
    {
        Text m_contentText;
        Button m_mallBtn;
        Button m_messageBtn;
        Button m_startBtn;
        Button m_logoutBtn;

        public MainPanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();

            m_mallBtn.onClick.AddListener(OnMallBtnClick);
            m_messageBtn.onClick.AddListener(OnMessageBtnClick);
            m_startBtn.onClick.AddListener(OnStartBtnClick);
            m_logoutBtn.onClick.AddListener(OnLogoutBtnClick);

            UIHelper.ShowPanel<BannerPanel>();
        }

        protected override void GetChild()
        {
            base.GetChild();
            m_contentText = transform.Find("ContentText").GetComponent<Text>();
            m_mallBtn = transform.Find("MallButton").GetComponent<Button>();
            m_messageBtn = transform.Find("MessageButton").GetComponent<Button>();
            m_startBtn = transform.Find("StartButton").GetComponent<Button>();
            m_logoutBtn = transform.Find("LogoutButton").GetComponent<Button>();
        }

        public override void Show()
        {
            base.Show();
            m_contentText.text = $"用户名:{data}";
        }

        void OnMallBtnClick()
        {
            UIHelper.ShowPanel<MallPanel>();
        }

        void OnMessageBtnClick()
        {
            UIHelper.ShowPanel<MessagePanel>();
        }

        void OnStartBtnClick()
        {
            SceneLoadManager.instance.LoadScene(GlobalDefine.GAME_SCENE_NAME);
        }

        void OnLogoutBtnClick()
        {
            UIHelper.ShowPanel<LoginPanel>();
        }
    }
}
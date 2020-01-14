using Hotfix.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("MessagePanel")]
    public class MessagePanel : UIPanel
    {
        Button m_closeBtn;

        Toggle m_informationToggle;
        Toggle m_albumToggle;
        Toggle m_journalToggle;

        InformationView m_InformationView;
        AlbumView m_albumView;
        JournalView m_journalView;

        public MessagePanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();

            m_informationToggle.onValueChanged.AddListener(OnInformationToggleClicked);
            m_albumToggle.onValueChanged.AddListener(OnAlbumToggleClicked);
            m_journalToggle.onValueChanged.AddListener(OnJournalToggleClicked);

            m_closeBtn.onClick.AddListener(OnCloseBtnClick);
        }

        public override void GetChild()
        {
            base.GetChild();
            m_closeBtn = transform.Find("CloseButton").GetComponent<Button>();

            m_informationToggle = transform.Find("Group/InformationToggle").GetComponent<Toggle>();
            m_albumToggle = transform.Find("Group/AlbumToggle").GetComponent<Toggle>();
            m_journalToggle = transform.Find("Group/JournalToggle").GetComponent<Toggle>();

            m_InformationView = UIViewManager.Instance.CreateView<InformationView>(transform.Find("Content/InformationContent").gameObject);
            m_albumView = UIViewManager.Instance.CreateView<AlbumView>(transform.Find("Content/AlbumContent").gameObject);
            m_journalView = UIViewManager.Instance.CreateView<JournalView>(transform.Find("Content/JournalContent").gameObject);
        }

        public override void Show()
        {
            base.Show();
            m_informationToggle.isOn = true;
        }

        void OnInformationToggleClicked(bool isSelect)
        {
            ShowSelectedView(4);
        }

        void OnAlbumToggleClicked(bool isSelect)
        {
            ShowSelectedView(2);
        }

        void OnJournalToggleClicked(bool isSelect)
        {
            ShowSelectedView(1);
        }

        void ShowSelectedView(int value)
        {
            m_InformationView.SetActive((4 & value) > 0);//100
            m_albumView.SetActive((2 & value) > 0);//010
            m_journalView.SetActive((1 & value) > 0);//001
        }

        void OnCloseBtnClick()
        {
            Hide();
        }
    }
}
using Hotfix.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("GamePanel")]
    public class GamePanel : UIPanel
    {
        GameObject m_stick;
        Button m_exitButton;
        Text m_timeText;

        float m_time;

        public GamePanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();

            m_exitButton.onClick.AddListener(OnExitButtonClick);
        }

        public override void Show()
        {
            base.Show();
            m_time = 300;
            m_timeText.text = $"{m_time / 60}:{m_time % 60}";
        }

        public override void Update()
        {
            base.Update();

            m_time -= Time.deltaTime;
            m_timeText.text = $"{(int)m_time / 60}:{(int)m_time % 60}";
        }

        protected override void GetChild()
        {
            base.GetChild();

            m_stick = transform.Find("Stick").gameObject;
            m_exitButton = transform.Find("ExitButton").GetComponent<Button>();
            m_timeText = transform.Find("Time/TimeText").GetComponent<Text>();
        }

        void OnExitButtonClick()
        {
            SceneLoadManager.instance.LoadScene(GlobalDefine.SAMPLE_SCENE_NAME);
        }
    }
}
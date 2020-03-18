using Hotfix.Manager;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("LoadingPanel", EUIPanelDepth.Loading, true)]
    public class LoadingPanel : UIPanel
    {
        Scrollbar m_progressBar;
        Text m_progressText;

        public LoadingPanel(string url) : base(url)
        {
        }

        public override void Show()
        {
            base.Show();

            SetProgress(0);
        }

        protected override void GetChild()
        {
            base.GetChild();
            m_progressBar = transform.Find("ProgressBar").GetComponent<Scrollbar>();
            m_progressText = transform.Find("ProgressText").GetComponent<Text>();
        }

        public void SetProgress(float value)
        {
            m_progressBar.size = value;
            m_progressText.text = $"{(int)(value * 100)}%";
        }
    }
}
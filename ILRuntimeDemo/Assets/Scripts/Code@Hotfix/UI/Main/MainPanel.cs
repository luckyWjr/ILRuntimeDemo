using Hotfix.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("MainPanel")]
    public class MainPanel : UIPanel
    {
        Text m_titleText;
        Text m_contentText;

        public MainPanel(string url) : base(url)
        {
            Debug.Log("MainPanel:" + url);
        }

        public override void Init()
        {
            base.Init();
            m_titleText = transform.Find("TitleText").GetComponent<Text>();
            m_contentText = transform.Find("ContentText").GetComponent<Text>();
        }
    }
}
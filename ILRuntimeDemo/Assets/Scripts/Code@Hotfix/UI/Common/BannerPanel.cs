﻿using Hotfix.Manager;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("BannerPanel")]
    public class BannerPanel : UIPanel
    {
        Button m_backButton;

        public BannerPanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();

            m_backButton.onClick.AddListener(OnBackButtonClicked);
        }

        public override void GetChild()
        {
            base.GetChild();
            m_backButton = transform.Find("BGImage/BackButton").GetComponent<Button>();
        }

        void OnBackButtonClicked()
        {
            UIPanelManager.instance.HidePanel();
        }
    }
}
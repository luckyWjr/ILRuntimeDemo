using Hotfix.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;
using Tool;

namespace Hotfix.UI
{
    public class DialogView : UIView
    {
        Text m_titleText;
        Text m_contentText;
        GameObject m_buttonGroupOne;
        GameObject m_buttonGroupTwo;
        Button m_groupOneConfirmButton;
        Button m_groupTwoConfirmButton;
        Button m_groupTwoCancelButton;
        Action m_confirmCallback;

        DialogType m_type;

        public DialogView(GameObject go) : base(go)
        {
            parent = UIPanelManager.instance.popupCanvas;
            rectTransform.ResetTrans();
        }

        public override void Init()
        {
            base.Init();

            m_groupOneConfirmButton.onClick.AddListener(OnCancelButtonClick);
            m_groupTwoConfirmButton.onClick.AddListener(OnConfirmButtonClick);
            m_groupTwoCancelButton.onClick.AddListener(OnCancelButtonClick);
        }

        public void Setting(DialogType type, string title, string content, Action confirmCallback)
        {
            if(type == DialogType.OnlyConfirm)
            {
                m_buttonGroupOne.SetActive(true);
                m_buttonGroupTwo.SetActive(false);
            }
            else
            {
                m_buttonGroupOne.SetActive(false);
                m_buttonGroupTwo.SetActive(true);
            }

            m_titleText.text = title;
            m_contentText.text = content;
            m_confirmCallback = confirmCallback;
        }

        protected override void GetChild()
        {
            base.GetChild();

            m_titleText = transform.Find("BGImage/TitleText").GetComponent<Text>();
            m_contentText = transform.Find("BGImage/ContentText").GetComponent<Text>();
            m_buttonGroupOne = transform.Find("BGImage/ButtonGroupOne").gameObject;
             m_buttonGroupTwo = transform.Find("BGImage/ButtonGroupTwo").gameObject;
            m_groupOneConfirmButton = transform.Find("BGImage/ButtonGroupOne/ConfirmButton").GetComponent<Button>();
            m_groupTwoConfirmButton = transform.Find("BGImage/ButtonGroupTwo/ConfirmButton").GetComponent<Button>();
            m_groupTwoCancelButton = transform.Find("BGImage/ButtonGroupTwo/CancelButton").GetComponent<Button>();
        }

        void OnCancelButtonClick()
        {
            Destroy();
        }

        void OnConfirmButtonClick()
        {
            Destroy();
            m_confirmCallback?.Invoke();
        }
    }
}
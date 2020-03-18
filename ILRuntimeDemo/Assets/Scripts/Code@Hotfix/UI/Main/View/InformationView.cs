using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    public class InformationView : UIView
    {
        Text m_nameText;
        Text m_sexText;
        Text m_phoneText;

        public InformationView(GameObject go) : base(go)
        {
        }

        public override void Init()
        {
            base.Init();

            m_nameText.text = "七龙珠";
            m_sexText.text = "成男";
            m_phoneText.text = "158xxxxx094";
        }

        protected override void GetChild()
        {
            base.GetChild();
            m_nameText = transform.Find("NameList/ContentText").GetComponent<Text>();
            m_sexText = transform.Find("SexList/ContentText").GetComponent<Text>();
            m_phoneText = transform.Find("PhoneList/ContentText").GetComponent<Text>();
        }
    }
}
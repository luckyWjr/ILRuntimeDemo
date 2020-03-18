using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    public class AlbumView : UIView
    {
        Text m_nameText;
        Button m_buyBtn;

        public AlbumView(GameObject go) : base(go)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        protected override void GetChild()
        {
            base.GetChild();
        }

        public void Setting(string name)
        {
            m_nameText.text = name;
        }

        void OnBuyBtnClicked()
        {
            Debug.Log("m_nameText:"+ m_nameText.text);
        }
    }
}
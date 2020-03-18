using Hotfix.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix.UI
{
    [UI("MallPanel")]
    public class MallPanel : UIPanel
    {
        ScrollRect m_scrollRect;
        GridLayoutGroup m_grid;

        List<string> m_mallList = new List<string>();

        public MallPanel(string url) : base(url)
        {
        }

        public override void Init()
        {
            base.Init();
            
            m_mallList.Add("明装");
            m_mallList.Add("燕尔");
            m_mallList.Add("西狩获麟");
            m_mallList.Add("刹那生灭");
            m_mallList.Add("听冰");
            m_mallList.Add("琅嬛");
            m_mallList.Add("陌上花");

            Object itemAsset = Resources.Load("GoodsItem");
            for (int i = 0; i < m_mallList.Count; i++)
            {
                GameObject go = GameObject.Instantiate(itemAsset) as GameObject;
                //m_grid.transform is RectTransform
                GoodsItemView view = UIViewManager.instance.CreateView<GoodsItemView>(go, m_grid.transform as RectTransform);
                view.Setting(m_mallList[i]);
                view.Show();
            }
        }

        protected override void GetChild()
        {
            base.GetChild();

            m_scrollRect = transform.Find("Scroll View").GetComponent<ScrollRect>();
            m_grid = transform.Find("Scroll View/Viewport/Content").GetComponent<GridLayoutGroup>();
        }

    }
}
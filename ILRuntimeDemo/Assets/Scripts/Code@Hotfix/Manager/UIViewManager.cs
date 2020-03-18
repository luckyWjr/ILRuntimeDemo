using Hotfix.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix.Manager
{
    public class UIViewManager : ManagerBase<UIViewManager>
    {
        //存放所有在场景中的UIView
        List<UIView> m_UIViewList;

        public override void Init()
        {
            base.Init();
            m_UIViewList = new List<UIView>();
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                //销毁UIView
                if (m_UIViewList[i].isWillDestroy)
                {
                    m_UIViewList[i].DestroyImmediately();
                    m_UIViewList.RemoveAt(i);
                    i--;
                    continue;
                }

                if (m_UIViewList[i].isVisible)
                {
                    m_UIViewList[i].Update();
                }
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                if (m_UIViewList[i].isVisible)
                {
                    m_UIViewList[i].LateUpdate();
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                if (m_UIViewList[i].isVisible)
                {
                    m_UIViewList[i].FixedUpdate();
                }
            }
        }

        public T CreateView<T>(GameObject go) where T : UIView
        {
            return CreateView(typeof(T), go) as T;
        }

        public T CreateView<T>(GameObject go, RectTransform parent) where T : UIView
        {
            return CreateView(typeof(T), go, parent) as T;
        }

        //创建UIView
        public UIView CreateView(Type type, params object[] args)
        {
            UIView view = Activator.CreateInstance(type, args) as UIView;
            view.Init();
            AddUIView(view);
            return view;
        }

        public void AddUIView(UIView view)
        {
            if (view != null)
                m_UIViewList.Add(view);
        }

        public void DestroyAll()
        {
            for (int i = m_UIViewList.Count - 1; i >= 0 ; i--)
                m_UIViewList[i].Destroy();
        }
        
        public void DestroyViewOnLoadScene()
        {
            for (int i = m_UIViewList.Count - 1; i >= 0 ; i--)
                if(!m_UIViewList[i].isDontDestroyOnLoad)
                    m_UIViewList[i].Destroy();
        }
    }
}

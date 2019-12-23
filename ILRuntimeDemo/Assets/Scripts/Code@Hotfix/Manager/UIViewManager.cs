using Hotfix.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix.Manager
{
    public class UIViewManager : ManagerBase<UIViewManager>
    {
        List<UIView> m_UIViewList;

        public override void Init()
        {
            base.Init();
            m_UIViewList = new List<UIView>();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < m_UIViewList.Count; i++)
            {
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

        public T CreateView<T>(string url) where T : UIView
        {
            return CreateView(typeof(T), url) as T;
        }

        public UIView CreateView(Type type, params object[] args)
        {
            //MissingMethodException: Constructor on type 'ILRuntime.Runtime.Intepreter.ILTypeInstance' not found.
            UIView view = Activator.CreateInstance(type, args) as UIView;
            m_UIViewList.Add(view);
            return view;
        }

        //public UIView GetView<T>(string id) where T : UIView
        //{
        //    if (string.IsNullOrEmpty(id)) return null;
        //    return m_UIViewList.Find(p => p.Id == id && !p.MaskAsDestroy);
        //}

        public void DestroyAll()
        {
            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                m_UIViewList[i].Destroy();
            }
        }
    }
}

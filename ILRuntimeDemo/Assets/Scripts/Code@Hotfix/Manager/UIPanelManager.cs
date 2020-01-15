using System;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using Hotfix.UI;

namespace Hotfix.Manager
{
    public class UIPanelManager : ManagerBaseWithAttr<UIPanelManager, UIAttribute>
    {
        public UIPanel currentPanel;//当前显示的页面

        Dictionary<string, UIPanel> m_UIPanelDic;//存放所有存在在场景中的UIPanel
        Transform m_UICanvas;
        public Transform uiPanelParent { get { return m_UICanvas; } }

        public override void Init()
        {
            base.Init();
            m_UIPanelDic = new Dictionary<string, UIPanel>();
            m_UICanvas = GameObject.Find("Canvas").transform;
        }

        public void ShowPanel<T>() where T : UIPanel
        {
            ShowPanel<T>(null, null);
        }

        public void ShowPanel<T>(Action<T> callback) where T : UIPanel
        {
            ShowPanel(callback, null);
        }

        public void ShowPanel<T>(object data) where T : UIPanel
        {
            ShowPanel<T>(null, data);
        }

        //显示一个UIPanel，参数为回调和自定义传递数据
        public void ShowPanel<T>(Action<T> callback, object data) where T : UIPanel
        {
            string url = GetUrl(typeof(T));
            if (!string.IsNullOrEmpty(url))
            {
                LoadPanel(url, data, () =>
                {
                    var panel = ShowPanel(url);
                    callback?.Invoke(panel as T);
                });
            }
        }

        //显示UIPanel
        UIPanel ShowPanel(string url)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                panel = m_UIPanelDic[url];
                panel.Show();
                currentPanel = panel;
            }
            else
                Debug.LogError("UIPanel not loaded:" + url);
            return panel;
        }

        //加载UIPanel对象
        public void LoadPanel(string url, object data, Action callback)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                if (panel.isVisible)
                    Debug.Log("UIPanel is visible:" + url);
                else
                    if (panel.isLoaded)
                        callback?.Invoke();
            }
            else
            {
                panel = CreatePanel(url);
                if (panel == null)
                    Debug.LogError("UIPanel not exist: " + url);
                else
                {
                    panel.data = data;
                    m_UIPanelDic[url] = panel;
                    panel.Load(() =>
                    {
                        if (panel.isLoaded)
                        {
                            panel.rectTransform.SetParentAndResetTrans(m_UICanvas);
                            callback?.Invoke();
                        }
                        else
                            m_UIPanelDic.Remove(url);
                    });
                }
            }
        }

        //实例化UIPanel对象
        UIPanel CreatePanel(string url)
        {
            var data = GetAtrributeData(url);
            if (data == null)
            {
                Debug.LogError("Unregistered UIPanel, unable to load: " + url);
                return null;
            }
            //var attr = data.attribute as UIAttribute;
            //var panel = UIViewManager.Instance.CreateView(data.type, attr.value) as UIPanel;

            var panel = CreateInstance<UIPanel>(url);
            UIViewManager.Instance.AddUIView(panel as UIView);
            return panel;
        }

        public void DestroyPanel<T>()
        {
            UnLoadPanel(GetUrl(typeof(T)));
        }

        void UnLoadPanel(string url)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                panel.Destroy();
                m_UIPanelDic.Remove(url);
            }
            else
                Debug.LogError("UIPanel not exist: " + url);
        }

        void UnLoadAllPanel()
        {
            foreach(var panel in m_UIPanelDic.Values)
                panel.Destroy();
            m_UIPanelDic.Clear();
        }

        //根据UIPanel的Type获取其对应的url
        string GetUrl(Type t)
        {
            foreach (var keyPairValue in m_atrributeDataDic)
                if (keyPairValue.Value.type == t)
                    return keyPairValue.Key;
            Debug.LogError($"Cannot found type({t.Name})");
            return null;
        }

        public override void OnApplicationQuit()
        {
            UnLoadAllPanel();
        }
    }
}

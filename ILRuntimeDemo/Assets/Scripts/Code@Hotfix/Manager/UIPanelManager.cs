using System;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using Hotfix.UI;

namespace Hotfix.Manager
{
    public class UIPanelManager : ManagerBaseWithAttr<UIPanelManager, UIAttribute>
    {
        Dictionary<string, UIPanel> m_UIPanelDic;
        Transform m_UICanvas;

        public override void Init()
        {
            base.Init();
            m_UIPanelDic = new Dictionary<string, UIPanel>();
            m_UICanvas = GameObject.Find("Canvas").transform;
        }

        public override void Start()
        {
            base.Start();
        }

        public void ShowPanel<T>(Action<T> callback = null) where T : UIPanel
        {
            string url = GetUrl(typeof(T));
            if (!string.IsNullOrEmpty(url))
            {
                LoadPanel(url, () =>
                {
                    var panel = ShowPanel(url);
                    callback?.Invoke(panel as T);
                });
            }
        }

        UIPanel ShowPanel(string url)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                panel = m_UIPanelDic[url];
                if (!panel.isVisible && panel.isLoaded)
                    panel.Show();
                else
                    Debug.Log("Window is in one of the[unload, visible] states：{0}" + url);
            }
            else
                Debug.LogError("Window not loaded" + url);
            return panel;
        }

        public void LoadPanel(string url, Action callback)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                if (panel.isLoaded)
                    callback?.Invoke();
            }
            else
            {
                panel = CreatePanel(url);
                if (panel == null)
                    Debug.LogError("Window not exist: " + url);
                else
                {
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

        UIPanel CreatePanel(string url)
        {
            var data = GetAtrributeData(url);
            if (data == null)
            {
                Debug.LogError("Unregistered window, unable to load: " + url);
                return null;
            }
            var attr = data.attribute as UIAttribute;
            var panel = UIViewManager.Instance.CreateView(data.type, attr.value) as UIPanel;
            return panel;
        }

        string GetUrl(Type t)
        {
            foreach (var keyPairValue in m_atrributeDataDic)
                if (keyPairValue.Value.type == t)
                    return keyPairValue.Key;
            Debug.LogError($"Cannot found type({t.Name})");
            return null;
        }
    }
}

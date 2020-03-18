using System;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using Hotfix.UI;

namespace Hotfix.Manager
{
    public enum EUIPanelDepth
    {
        Default,
        Banner,
        Loading,
        Popup,
    }

    public class UIPanelManager : ManagerBaseWithAttr<UIPanelManager, UIAttribute>
    {
        public UIPanel currentPanel;//当前显示的页面

        Dictionary<string, UIPanel> m_UIPanelDic;//存放所有存在在场景中的UIPanel

        Transform m_uiRoot;
        Transform m_defaultCanvas;
        public Transform defaultCanvas { get { return m_defaultCanvas; } }
        Transform m_bannerCanvas;
        public Transform bannerCanvas { get { return m_bannerCanvas; } }
        Transform m_loadingCanvas;
        public Transform loadingCanvas { get { return m_loadingCanvas; } }
        Transform m_popupCanvas;
        public Transform popupCanvas { get { return m_popupCanvas; } }

        public override void Init()
        {
            base.Init();
            m_UIPanelDic = new Dictionary<string, UIPanel>();

            m_uiRoot = GameObject.Find(GlobalDefine.UI_ROOT_NAME).transform;
            GameObject.DontDestroyOnLoad(m_uiRoot);
            m_defaultCanvas = GameObject.Find(GlobalDefine.UI_DEFAULT_CANVAS_NAME).transform;
            m_bannerCanvas = GameObject.Find(GlobalDefine.UI_BANNER_CANVAS_NAME).transform;
            m_loadingCanvas = GameObject.Find(GlobalDefine.UI_LOADING_CANVAS_NAME).transform;
            m_popupCanvas = GameObject.Find(GlobalDefine.UI_POPUP_CANVAS_NAME).transform;
        }

        //显示一个UIPanel，参数为回调和自定义传递数据
        public void ShowPanel<T>(Action<T> callback, object data) where T : UIPanel
        {
            if(GetUIMessage(typeof(T), out string url, out EUIPanelDepth depth, out bool isDontDestroyOnLoad));
            {
                LoadPanel(url, depth, isDontDestroyOnLoad, data, () =>
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
        public void LoadPanel(string url, EUIPanelDepth depth, bool isDontDestroyOnLoad,object data, Action callback)
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
                    panel.isDontDestroyOnLoad = isDontDestroyOnLoad;
                    m_UIPanelDic[url] = panel;
                    panel.Load(() =>
                    {
                        if (panel.isLoaded)
                        {
                            if(depth == EUIPanelDepth.Banner)
                                panel.rectTransform.SetParentAndResetTrans(m_bannerCanvas);
                            else if(depth == EUIPanelDepth.Loading)
                                panel.rectTransform.SetParentAndResetTrans(m_loadingCanvas);
                            else
                                panel.rectTransform.SetParentAndResetTrans(m_defaultCanvas);
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
            UIViewManager.instance.AddUIView(panel);
            return panel;
        }

        public void HidePanel()
        {
            currentPanel?.Hide();
        }

        public void DestroyPanel<T>()
        {
            UnLoadPanel(GetUrl(typeof(T)));
        }

        public void UnLoadPanelOnLoadScene()
        {
            List<string> list = new List<string>();
            foreach (var panel in m_UIPanelDic.Values)
                if (!panel.isDontDestroyOnLoad)
                    list.Add(panel.url);
            
            foreach (var url in list)
                UnLoadPanel(url);
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

        //根据UIPanel的Type获取其对应的url和depth
        string GetUrl(Type t)
        {
            foreach (var keyPairValue in m_atrributeDataDic)
                if (keyPairValue.Value.type == t)
                    return keyPairValue.Key;
            Debug.LogError($"Cannot found type({t.Name})");
            return null;
        }
        
        bool GetUIMessage(Type t, out string url, out EUIPanelDepth depth, out bool isDontDestroyOnLoad)
        {
            url = "";
            depth = EUIPanelDepth.Default;
            isDontDestroyOnLoad = false;
            foreach (var keyPairValue in m_atrributeDataDic)
            {
                if (keyPairValue.Value.type == t)
                {
                    UIAttribute attr = keyPairValue.Value.attribute as UIAttribute;
                    if (attr != null)
                    {
                        url = attr.value;
                        depth = attr.depth;
                        isDontDestroyOnLoad = attr.isDontDestroyOnLoad;
                        return true;
                    }
                }
            }
            Debug.LogError($"Cannot found type({t.Name})");
            return false;
        }

        public override void OnApplicationQuit()
        {
            UnLoadAllPanel();
        }
    }
}

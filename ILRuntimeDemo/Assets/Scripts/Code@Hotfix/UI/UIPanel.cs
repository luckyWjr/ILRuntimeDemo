using System;
using UnityEngine;

namespace Hotfix.UI
{
    public class UIPanel : UIView
    {
        //需要加载的prefab的路径，也作为唯一标识符
        public string url { private set; get; }

        //UIPanel间的自定义传递数据
        public object data;

        public UIPanel(string url)
        {
            this.url = url;
        }

        //加载prefab
        public virtual void Load(Action callback = null)
        {
            GameObject gameObject = GameObject.Instantiate(Resources.Load(url)) as GameObject;
            if (gameObject != null)
            {
                SetGameObject(gameObject);
                Init();
                callback?.Invoke();
            }
        }
    }
}
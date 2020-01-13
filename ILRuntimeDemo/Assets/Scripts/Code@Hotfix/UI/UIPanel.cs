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

        //前一个UIPanel，用于隐藏自己的时候，Show前者
        public UIPanel previousPanel;

        public UIPanel(string url)
        {
            this.url = url;
        }

        public override void Destroy()
        {
            base.Destroy();
            previousPanel = null;
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
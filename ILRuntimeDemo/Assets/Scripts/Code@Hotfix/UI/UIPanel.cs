using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hotfix.UI
{
    public class UIPanel : UIView
    {
        //需要加载的prefab的路径，也作为唯一标识符
        public string url { get; }

        //UIPanel间的自定义传递数据
        public object data;

        public UIPanel(string url)
        {
            this.url = url;
        }

        public override void Show()
        {
            base.Show();
            rectTransform.SetAsLastSibling();
        }

        //加载prefab
        public virtual void Load(Action callback = null)
        {
            GameObject gameObject = Object.Instantiate(Resources.Load(url)) as GameObject;
            if (gameObject != null)
            {
                SetGameObject(gameObject);
                Init();
                callback?.Invoke();
            }
        }
    }
}
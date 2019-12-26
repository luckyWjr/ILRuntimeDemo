using System;
using UnityEngine;

namespace Hotfix.UI
{
    public class UIView : IView
    {
        //需要加载的prefab的路径，也作为唯一标识符
        public string url { private set; get; }

        public GameObject gameObject { private set; get; }
        public Transform transform { private set; get; }
        public RectTransform rectTransform { private set; get; }

        //是否加载完成
        public bool isLoaded { get { return gameObject != null; } }

        //是否显示
        public bool isVisible
        {
            get
            {
                return isLoaded && gameObject.activeSelf;
            }
            set
            {
                if (isLoaded)
                    gameObject.SetActive(value);
            }
        }

        //若为true，将在下一帧销毁gameobject
        internal bool isWillDestroy;

        public UIView(string url)
        {
            this.url = url;
        }

        public virtual void Init()
        {
            isVisible = false;
        }

        public virtual void Show()
        {
            isVisible = true;
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Hide()
        {
            isVisible = false;
        }

        public virtual void Destroy()
        {
            isWillDestroy = true;
            if (isVisible)
            {
                Hide();
            }
        }

        //销毁gameobject
        public void DestroyImmediately()
        {
            if (!isWillDestroy)
            {
                Destroy();
            }
            GameObject.Destroy(gameObject);
            gameObject = null;
            transform = null;
            rectTransform = null;
        }

        //加载prefab
        public virtual void Load(Action callback = null)
        {
            gameObject = GameObject.Instantiate(Resources.Load(url)) as GameObject;
            if (gameObject != null)
            {
                transform = gameObject.transform;
                rectTransform = gameObject.GetComponent<RectTransform>();

                Init();
                callback?.Invoke();
            }
        }
    }
}
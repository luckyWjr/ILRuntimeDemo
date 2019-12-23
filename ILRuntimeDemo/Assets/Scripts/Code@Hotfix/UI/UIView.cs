using System;
using UnityEngine;

namespace Hotfix.UI
{
    public class UIView : IView
    {
        string mUrl;

        public bool isVisible;
        public GameObject gameObject { private set; get; }
        public Transform transform { private set; get; }
        public RectTransform rectTransform { private set; get; }

        public bool isLoaded { get { return gameObject != null; } }

        public UIView(string url)
        {
            mUrl = url;
        }

        public virtual void Destroy()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Hide()
        {
        }

        public virtual void Init()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void Show()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Load(Action callback = null)
        {
            gameObject = GameObject.Instantiate(Resources.Load(mUrl)) as GameObject;
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
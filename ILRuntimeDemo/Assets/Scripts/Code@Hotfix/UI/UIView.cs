using System;
using UnityEngine;

namespace Hotfix.UI
{
    public class UIView : IView
    {
        string m_url;

        public GameObject gameObject { private set; get; }
        public Transform transform { private set; get; }
        public RectTransform rectTransform { private set; get; }

        public bool isLoaded { get { return gameObject != null; } }
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

        public UIView(string url)
        {
            m_url = url;
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

        public virtual void FixedUpdate()
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual void Hide()
        {
            isVisible = false;
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void Load(Action callback = null)
        {
            gameObject = GameObject.Instantiate(Resources.Load(m_url)) as GameObject;
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
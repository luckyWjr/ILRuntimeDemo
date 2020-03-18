using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hotfix.UI
{
    public class UIView : IView
    {
        public GameObject gameObject { private set; get; }
        public Transform transform { private set; get; }
        public RectTransform rectTransform { private set; get; }

        Transform m_parent;
        public Transform parent
        {
            get { return m_parent; }
            set
            {
                m_parent = value;
                if (m_parent != null)
                    transform?.SetParent(m_parent);
            }
        }

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

        //在load scene的时候是否销毁
        public bool isDontDestroyOnLoad;

        public UIView()
        {
        }

        public UIView(GameObject go)
        {
            SetGameObject(go);
        }

        public UIView(GameObject go, Transform parent)
        {
            SetGameObject(go, parent);
        }

        protected void SetGameObject(GameObject go, Transform parent = null)
        {
            gameObject = go;
            if (gameObject == null)
                return;
            transform = gameObject.transform;
            rectTransform = gameObject.GetComponent<RectTransform>();
            this.parent = parent;
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
                Show();
            else
                Hide();
        }

        public virtual void Init()
        {
            GetChild();
        }

        protected virtual void GetChild()
        {
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
                Hide();
        }

        //销毁gameobject
        public void DestroyImmediately()
        {
            if (!isWillDestroy)
                Destroy();
            Object.Destroy(gameObject);
            gameObject = null;
            transform = null;
            rectTransform = null;
        }
    }
}
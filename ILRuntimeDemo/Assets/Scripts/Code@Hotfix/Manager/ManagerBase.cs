using System;

namespace Hotfix.Manager
{
    public class ManagerBase<T> : IManager where T : IManager, new()
    {
        protected static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new T();
                }
                return mInstance;
            }
        }

        protected ManagerBase()
        {
        }

        public virtual void Init()
        {

        }

        public virtual void Start()
        {

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

        public virtual void OnApplicationQuit()
        {

        }
    }
}

namespace Hotfix.Manager
{
    class ManagerBase<T> : ILifeCycle where T : ILifeCycle, new()
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

        virtual public void Awake()
        {

        }

        virtual public void Start()
        {

        }

        virtual public void Update()
        {

        }

        virtual public void LateUpdate()
        {

        }

        virtual public void FixedUpdate()
        {

        }

        virtual public void Destroy()
        {

        }
    }
}

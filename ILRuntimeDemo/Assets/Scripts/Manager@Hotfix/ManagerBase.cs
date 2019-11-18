namespace Hotfix.Manager
{
    public class ManagerBase<T> : ILifeCycle where T : ILifeCycle, new()
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

        public virtual void Awake()
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

        public virtual void Destroy()
        {

        }

        public virtual void OnApplicationQuit()
        {

        }
    }
}

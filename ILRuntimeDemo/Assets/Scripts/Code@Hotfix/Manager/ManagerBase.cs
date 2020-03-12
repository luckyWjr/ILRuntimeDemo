namespace Hotfix.Manager
{
    public class ManagerBase<T> : IManager where T : IManager, new()
    {
        protected static T m_instance;

        public static T instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new T();
                }
                return m_instance;
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

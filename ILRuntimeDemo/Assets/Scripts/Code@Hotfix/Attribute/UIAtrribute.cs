namespace Hotfix.Manager
{
    public class UIAttribute : ManagerAttribute
    {
        public readonly EUIPanelDepth depth;
        public readonly bool isDontDestroyOnLoad;

        public UIAttribute(string url) : base(url)
        {
            depth = EUIPanelDepth.Default;
            isDontDestroyOnLoad = false;
        }
        
        public UIAttribute(string url, EUIPanelDepth depth) : base(url)
        {
            this.depth = depth;
            isDontDestroyOnLoad = false;
        }
        
        public UIAttribute(string url, EUIPanelDepth depth, bool isDontDestroyOnLoad) : base(url)
        {
            this.depth = depth;
            this.isDontDestroyOnLoad = isDontDestroyOnLoad;
        }
    }
}


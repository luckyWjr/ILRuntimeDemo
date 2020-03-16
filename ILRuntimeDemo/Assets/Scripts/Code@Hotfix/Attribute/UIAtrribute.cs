namespace Hotfix.Manager
{
    public class UIAttribute : ManagerAttribute
    {
        public EUIPanelDepth depth;

        public UIAttribute(string url) : base(url)
        {
            depth = EUIPanelDepth.Default;
        }
        
        public UIAttribute(string url, EUIPanelDepth depth) : base(url)
        {
            this.depth = depth;
        }
    }
}


namespace Hotfix.UI
{
    public class UIPanel : UIView
    {
        //UIPanel间的自定义传递数据
        public object data;

        //前一个UIPanel，用于隐藏自己的时候，Show前者
        public UIPanel previousPanel;

        public UIPanel(string url) : base(url)
        {
        }

        public override void Destroy()
        {
            base.Destroy();
            previousPanel = null;
        }
    }
}
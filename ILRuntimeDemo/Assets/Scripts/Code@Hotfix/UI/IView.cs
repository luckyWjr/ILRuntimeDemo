namespace Hotfix.UI
{
    public interface IView

    {
        //初始化，只在prefab被创建的时候执行一次
        void Init();
        //每次界面显示的时候执行
        void Show();
        void Update();
        void LateUpdate();
        void FixedUpdate();
        //界面被隐藏的时候执行，再次显示会调用Show方法
        void Hide();
        //销毁的时候执行
        void Destroy();
    }
}
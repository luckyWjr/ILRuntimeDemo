namespace Hotfix.UI
{
    public interface IView

    {
        void Init();
        void Show();
        void Hide();
        void Destroy();
        void Update();
        void LateUpdate();
        void FixedUpdate();
    }
}


namespace Hotfix.Manager
{
    public interface IManager
    {
        void Init();
        void Start();
        void Update();
        void LateUpdate();
        void FixedUpdate();
        void OnApplicationQuit();
    }
}
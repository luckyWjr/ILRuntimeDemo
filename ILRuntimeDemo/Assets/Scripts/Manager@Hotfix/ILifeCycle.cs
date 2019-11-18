using System;
namespace Hotfix.Manager
{
    public interface ILifeCycle
    {
        void Awake();
        void Start();
        void Update();
        void LateUpdate();
        void FixedUpdate();
        void Destroy();
        void OnApplicationQuit();
    }
}
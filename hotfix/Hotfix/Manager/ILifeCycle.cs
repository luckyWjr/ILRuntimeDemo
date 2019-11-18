using System;
namespace Hotfix.Manager
{
    interface ILifeCycle
    {
        void Awake();
        void Start();
        void Update();
        void LateUpdate();
        void FixedUpdate();
        void Destroy();
    }
}
using UnityEngine;

namespace Hotfix.Manager
{
    public class UIManager : ManagerBase<UIManager>
    {
        public override void Start()
        {
            base.Start();
            Debug.Log("UIManager start");
        }
    }
}

using UnityEngine;

namespace Hotfix.Manager
{
    class TimeManager : ManagerBase<TimeManager>
    {
        public override void Start()
        {
            base.Start();
            Debug.Log("TimeManager start");
        }
    }
}

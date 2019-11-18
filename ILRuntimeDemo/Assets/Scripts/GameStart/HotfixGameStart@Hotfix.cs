using Hotfix.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttributeTool;

namespace Hotfix
{
    [GameStartAtrribute(true)]
    public class HotfixGameStart : ILifeCycle
    {
        List<ILifeCycle> managerList = new List<ILifeCycle>();

        public void Awake()
        {
        }

        public void Start()
        {
            ReigsterAllManager();
        }

        void ReigsterAllManager()
        {
            managerList.Clear();
            managerList.Add(TimeManager.Instance);
            managerList.Add(UIManager.Instance);
        }

        public void Update()
        {
            foreach (var manager in managerList)
            {
                manager.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (var manager in managerList)
            {
                manager.LateUpdate();
            }
        }

        public void FixedUpdate()
        {
            foreach (var manager in managerList)
            {
                manager.FixedUpdate();
            }
        }

        public void OnApplicationQuit()
        {
            Debug.Log("Hotfix ApplicationQuit");
        }

        public void Destroy()
        {
        }
    }
}
using Hotfix.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using Tool;
using UnityEngine;

namespace Hotfix
{
    class HotfixLaunch
    {
        static List<ILifeCycle> managerList = new List<ILifeCycle>();

        public static void Start()
        {
            Debug.Log("HotfixLaunch Start");
            //获取Hotfix.dll内部定义的类
            List<Type> allTypes = new List<Type>();
            var values = ILRuntimeHelp.appdomain.LoadedTypes.Values.ToList();
            foreach (var v in values)
            {
                allTypes.Add(v.ReflectionType);
            }
            //去重
            allTypes = allTypes.Distinct().ToList();

            //获取hotfix的管理类，并启动
            foreach (var t in allTypes)
            {
                try
                {
                    if (t != null && t.BaseType != null && t.BaseType.FullName != null)
                    {
                        if (t.BaseType.FullName.Contains(".ManagerBase`"))
                        {
                            Debug.Log("加载管理器-" + t);
                            var manager = t.BaseType.GetProperty("Instance").GetValue(null, null) as ILifeCycle;
                            manager.Start();
                            managerList.Add(manager);
                            continue;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            //绑定生命周期方法
            Launch.OnUpdate = Update;
            Launch.OnLateUpdate = LateUpdate;
            Launch.OnFixedUpdate = FixedUpdate;
            Launch.OnApplicationQuitAction = ApplicationQuit;
        }

        static void Update()
        {
            foreach(var manager in managerList)
            {
                manager.Update();
            }
        }

        static void LateUpdate()
        {
            foreach (var manager in managerList)
            {
                manager.LateUpdate();
            }
        }

        static void FixedUpdate()
        {
            foreach (var manager in managerList)
            {
                manager.FixedUpdate();
            }
        }

        static void ApplicationQuit()
        {
            Debug.Log("hotfix ApplicationQuit");
        }
    }
}

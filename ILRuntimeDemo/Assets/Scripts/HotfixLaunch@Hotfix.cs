using Hotfix.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tool;
using UnityEngine;

namespace Hotfix
{
    public class HotfixLaunch
    {
        static List<ILifeCycle> managerList = new List<ILifeCycle>();

        public static void Start(bool isHotfix)
        {
            Debug.Log("HotfixLaunch Start");
            //获取Hotfix.dll内部定义的类
            List<Type> allTypes = new List<Type>();

            if (isHotfix)
            {
                var values = ILRuntimeHelp.appdomain.LoadedTypes.Values.ToList();
                foreach (var v in values)
                {
                    allTypes.Add(v.ReflectionType);
                }
            }
            else
            {
                var assembly = Assembly.GetAssembly(typeof(HotfixLaunch));
                if (assembly == null)
                {
                    Debug.LogError("当前dll is null");
                    return;
                }
                allTypes = assembly.GetTypes().ToList();
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
            Launch.OnApplicationQuitAction = OnApplicationQuit;
        }

        public static void Update()
        {
            foreach (var manager in managerList)
            {
                manager.Update();
            }
        }

        public static void LateUpdate()
        {
            foreach (var manager in managerList)
            {
                manager.LateUpdate();
            }
        }

        public static void FixedUpdate()
        {
            foreach (var manager in managerList)
            {
                manager.FixedUpdate();
            }
        }

        public static void OnApplicationQuit()
        {
            Debug.Log("Hotfix ApplicationQuit");
        }
    }
}

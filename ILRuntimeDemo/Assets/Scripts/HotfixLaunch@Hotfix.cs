using AttributeTool;
using Hotfix.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using Tool;
using UnityEngine;

namespace Hotfix
{
    public class HotfixLaunch
    {
        static List<ILifeCycle> managerList = new List<ILifeCycle>();
        static ILifeCycle gameStart;

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

                    if(gameStart == null)
                    {
                        var attrs = t.GetCustomAttributes(typeof(GameStartAtrribute), false);
                        if (attrs.Length > 0)
                        {
                            var gameStartAttr = attrs[0] as GameStartAtrribute;
                            if (gameStartAttr != null && gameStartAttr.isHotfix)
                            {
                                gameStart = Activator.CreateInstance(t) as ILifeCycle;
                                Debug.Log("找到启动器" + t.FullName);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            //绑定生命周期方法
            Launch.OnUpdate = gameStart.Update;
            Launch.OnLateUpdate = gameStart.LateUpdate;
            Launch.OnFixedUpdate = gameStart.FixedUpdate;
            Launch.OnApplicationQuitAction = gameStart.OnApplicationQuit;
        }
    }
}

using Hotfix.Manager;
using Hotfix.UI;
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
        static List<IManager> m_managerList = new List<IManager>();

        public static void Start(bool isHotfix)
        {
            //获取Hotfix.dll内部定义的类
            List<Type> allTypes = new List<Type>();

            if (isHotfix)
            {
                var values = ILRuntimeHelp.appdomain.LoadedTypes.Values.ToList();
                foreach (var v in values)
                    allTypes.Add(v.ReflectionType);
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

            var attributeManagerList = new List<IAttribute>();

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
                            var manager = t.BaseType.GetProperty("instance").GetValue(null, null) as IManager;
                            m_managerList.Add(manager);
                            continue;
                        }
                        else if (t.BaseType.FullName.Contains(".ManagerBaseWithAttr`"))
                        {
                            Debug.Log("加载管理器-" + t);
                            var manager = t.BaseType.BaseType.GetProperty("instance").GetValue(null, null) as IManager;
                            m_managerList.Add(manager);
                            attributeManagerList.Add(manager as IAttribute);
                            continue;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            //遍历所有类和ManagerBaseWithAttr管理器，找出对应的被ManagerBaseWithAttr管理的子类。例如UIPanelManager和LoginPane的关系
            foreach (var t in allTypes)
                foreach (var attr in attributeManagerList)
                    attr.CheckType(t);

            foreach (var manager in m_managerList)
                manager.Init();

            //绑定生命周期方法
            Launch.OnUpdate = Update;
            Launch.OnLateUpdate = LateUpdate;
            Launch.OnFixedUpdate = FixedUpdate;
            Launch.OnApplicationQuitAction = OnApplicationQuit;

            foreach (var manager in m_managerList)
                manager.Start();

            UIHelper.ShowPanel<LoginPanel>();
        }

        public static void Update()
        {
            foreach (var manager in m_managerList)
                manager.Update();
        }

        public static void LateUpdate()
        {
            foreach (var manager in m_managerList)
                manager.LateUpdate();
        }

        public static void FixedUpdate()
        {
            foreach (var manager in m_managerList)
                manager.FixedUpdate();
        }

        public static void OnApplicationQuit()
        {
            Debug.Log("Hotfix ApplicationQuit");
        }
    }
}

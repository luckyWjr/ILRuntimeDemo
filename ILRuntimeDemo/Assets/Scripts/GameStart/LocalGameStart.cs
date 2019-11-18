using AttributeTool;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Unity
{
    [GameStartAtrribute(false)]
    public class LocalGameStart
	{
		void Start()
		{
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType("HotfixLaunch");
            var method = type.GetMethod("Start", BindingFlags.Public | BindingFlags.Static);
            method.Invoke(null, null);
            //不直接使用 HotfixLaunch.Start 是防止编dll的时候报错
        }

        void Update()
		{
			
		}
	}
}


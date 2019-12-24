using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Tool
{
	public class ILRuntimeHelp
	{
        public static ILRuntime.Runtime.Enviorment.AppDomain appdomain;
        static MemoryStream m_hotfixMemoryStream;

        public static IEnumerator LoadILRuntime(Action LoadedFinish)
        {
            appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();

            //把官方文档的WWW用UnityWebRequest替代了
            UnityWebRequest webRequest = UnityWebRequest.Get("file:///" + Application.streamingAssetsPath + "/hotfix_dll/Hotfix.dll");
            yield return webRequest.SendWebRequest();

            byte[] dll = null;
            if (webRequest.isNetworkError)
                Debug.Log("Download Error:" + webRequest.error);
            else
                dll = webRequest.downloadHandler.data;

            //用下面的会报错：ObjectDisposedException: Cannot access a closed Stream.
            //using (MemoryStream fs = new MemoryStream(dll))
            //{
            //    appdomain.LoadAssembly(fs);
            //}

            m_hotfixMemoryStream = new MemoryStream(dll);
            appdomain.LoadAssembly(m_hotfixMemoryStream);

            webRequest.Dispose();
            webRequest = null;
            ILRuntimeDelegateHelp.RegisterDelegate(appdomain);
            ILRuntimeAdapterHelp.RegisterCrossBindingAdaptor(appdomain);
            ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);

            //用于ILRuntime Debug
            if (Application.isEditor)
                appdomain.DebugService.StartDebugService(56000);

            LoadedFinish?.Invoke();
        }

        public static void Dispose()
        {
            m_hotfixMemoryStream?.Dispose();
        }
    }
}
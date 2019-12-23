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

        public static IEnumerator LoadILRuntime(Action LoadedFinish)
        {
            appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();

            //把官方文档的WWW用UnityWebRequest替代了
            UnityWebRequest webRequest = UnityWebRequest.Get("file:///" + Application.streamingAssetsPath + "/hotfix_dll/Hotfix.dll");
            yield return webRequest.SendWebRequest();

            byte[] dll = null;
            if (webRequest.isNetworkError)
            {
                Debug.Log("Download Error:" + webRequest.error);
            }
            else
            {
                dll = webRequest.downloadHandler.data;
            }
            //webRequest.Dispose();

            //webRequest = UnityWebRequest.Get("file:///" + Application.streamingAssetsPath + "/hotfix_dll/Hotfix.pdb");
            //yield return webRequest.SendWebRequest();

            //byte[] pdb = null;
            //if (webRequest.isNetworkError)
            //{
            //    Debug.Log("Download Error:" + webRequest.error);
            //}
            //else
            //{
            //    pdb = webRequest.downloadHandler.data;
            //}

            using (MemoryStream fs = new MemoryStream(dll))
            {
                //using (MemoryStream p = new MemoryStream(pdb))
                //{
                    //appdomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
                appdomain.LoadAssembly(fs);
                //}
            }

            webRequest.Dispose();
            webRequest = null;

            ILRuntimeDelegateHelp.RegisterDelegate(appdomain);
            ILRuntimeAdapterHelp.RegisterCrossBindingAdaptor(appdomain);
            ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
            appdomain.DebugService.StartDebugService(56000);
            LoadedFinish?.Invoke();
        }
    }
}
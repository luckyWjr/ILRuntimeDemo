using UnityEngine;

namespace Tool
{
	public class ILRuntimeAdapterHelp
    {
        //跨域继承绑定适配器
        public static void RegisterCrossBindingAdaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        }
    }
}
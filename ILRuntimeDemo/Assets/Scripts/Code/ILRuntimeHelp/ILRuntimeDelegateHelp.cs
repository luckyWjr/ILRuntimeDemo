using System;

namespace Tool
{
	public class ILRuntimeDelegateHelp
	{
        //跨域委托调用，注册委托的适配器
        public static void RegisterDelegate(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterMethodDelegate<System.Boolean>();
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
            {
                return new UnityEngine.Events.UnityAction(() =>
                {
                    ((Action)act)();
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.Boolean>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.Boolean>((arg0) =>
                {
                    ((Action<System.Boolean>)act)(arg0);
                });
            });
        }
    }
}


using System;
using System.Reflection;
using Tool;
using UnityEngine;

public enum AssetLoadMethod
{
    Editor = 0,
    StreamingAsset,
}

public class Launch : MonoBehaviour
{
    public AssetLoadMethod ILRuntimeCodeLoadMethod;

    public static Action OnUpdate { get; set; }
    public static Action OnLateUpdate { get; set; }
    public static Action OnFixedUpdate { get; set; }
    public static Action OnApplicationQuitAction { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        if(ILRuntimeCodeLoadMethod == AssetLoadMethod.StreamingAsset)
            StartCoroutine(ILRuntimeHelp.LoadILRuntime(OnILRuntimeInitialized));
        else
        {
            //不直接调用 Hotfix.HotfixLaunch.Start 是防止编译dll的时候找不到 Hotfix部分的Hotfix.HotfixLaunch类而报错
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType("Hotfix.HotfixLaunch");
            var method = type.GetMethod("Start", BindingFlags.Public | BindingFlags.Static);
            method.Invoke(null, new object[] { false });
        }
    }

    void OnILRuntimeInitialized()
    {
        ILRuntimeHelp.appdomain.Invoke("Hotfix.HotfixLaunch", "Start", null, new object[] { true });
    }

    void Update()
    {
        OnUpdate?.Invoke();
    }

    void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }

    void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    void OnApplicationQuit()
    {
        OnApplicationQuitAction?.Invoke();

        ILRuntimeHelp.Dispose();
        GC.Collect();
    }
}
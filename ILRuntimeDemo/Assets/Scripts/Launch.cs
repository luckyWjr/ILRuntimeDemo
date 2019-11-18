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
        if(ILRuntimeCodeLoadMethod == AssetLoadMethod.StreamingAsset)
        {
            StartCoroutine(ILRuntimeHelp.LoadILRuntime(OnILRuntimeInitialized));
        }
        else
        {
            
        }
    }

    void OnILRuntimeInitialized()
    {
        ILRuntimeHelp.appdomain.Invoke("Hotfix.HotfixLaunch", "Start", null, null);
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
    }
}
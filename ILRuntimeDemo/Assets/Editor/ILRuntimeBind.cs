using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ILRuntimeBind
{
    [MenuItem("ILRuntime/Generate CLR Binding Code")]
    static void GenerateCLRBinding()
    {
        List<Type> types = new List<Type>();
        //在List中添加你想进行CLR绑定的类型
        //types.Add(typeof(IUI));
        //types.Add(typeof(IComponent));
        //第二个参数为自动生成的代码保存在何处
        ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types, "Assets/ILRuntime/Generated");
        AssetDatabase.Refresh();
    }
}

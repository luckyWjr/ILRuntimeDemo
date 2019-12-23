using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace EditorTool
{
	public class ILRuntimeWindow : EditorWindow
    {
        Vector2 mLogScroll;
        string mLogs = string.Empty;

        public void OnGUI()
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("脚本打包");
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("1.编译 Hotfix.dll", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        mLogs = string.Empty;
                        string outpath = Application.streamingAssetsPath + "/hotfix_dll/";
                        BuildDLL(Application.dataPath + "/", outpath);
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("2.生成绑定代码", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        mLogs = string.Empty;
                        GenerateCLRBinding();
                        mLogs = "生成成功";
                    }
                }
                GUILayout.EndHorizontal();

                if (!string.IsNullOrEmpty(mLogs))
                {
                    mLogScroll = EditorGUILayout.BeginScrollView(mLogScroll, GUILayout.Height(400));
                    mLogs = EditorGUILayout.TextArea(mLogs);
                    EditorGUILayout.EndScrollView();
                }
            }
            GUILayout.EndVertical();
        }

        public void BuildDLL(string codeSource, string export, Action compileFinishedCallback = null, Action<string> outPutReceivedEvent = null)
        {
            string exePath = Environment.CurrentDirectory + "/Tools/BuildHotfixDll/BuildDllTool.exe";
            if (!File.Exists(exePath))
            {
                Debug.Log("编译工具不存在!");
                return;
            }

            //这里是引入unity所有引用的dll
            var u3dUI = string.Format(@"{0}\UnityExtensions\Unity", EditorApplication.applicationContentsPath);
            var u3dEngine = string.Format(@"{0}\Managed\UnityEngine", EditorApplication.applicationContentsPath);
            string libDll = Environment.CurrentDirectory + "/Library/ScriptAssemblies";
            string dllPath = u3dUI + "," + u3dEngine + "," + libDll;

            if (Directory.Exists(u3dUI) == false || Directory.Exists(u3dEngine) == false || Directory.Exists(libDll) == false)
            {
                EditorUtility.DisplayDialog("提示", "dll文件目录不存在,请修改ILRuntimeBuildWindow类中,u3dUI u3dEngine libDll的dll目录", "OK");
                return;
            }

            //编译配置文件目录
            string compilerDirectoryPath = Environment.CurrentDirectory + "/Tools/BuildHotfixDll/roslyn";

            var define = GetScriptingDefineSymbols();

            //执行exe文件，传递参数
            var p = new Process();
            p.EnableRaisingEvents = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = exePath;
            p.StartInfo.Arguments = string.Format("{0} {1} {2} {3} {4}", codeSource, export, dllPath, compilerDirectoryPath, define);
            p.Exited += (sender, e) =>
            {
                compileFinishedCallback?.Invoke();
            };
            p.OutputDataReceived += (sender, e) =>
            {
                mLogs += (e.Data + "\n");
            };
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("gb2312");
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
            EditorUtility.ClearProgressBar();

            AssetDatabase.Refresh();
        }

        //获取编译选项
        string GetScriptingDefineSymbols()
        {
            List<string> validDefines = new List<string>();
            foreach (var define in EditorUserBuildSettings.activeScriptCompilationDefines)
            {
                if (!define.Contains("UNITY_EDITOR"))
                {
                    validDefines.Add(define);
                }
            }
            return string.Join(";", validDefines);
        }


        const string BindingPath = "Assets/Scripts/Code/ILRuntimeHelp/BindingCode";
        void GenerateCLRBinding()
        {
            List<Type> types = new List<Type>();
            //在List中添加你想进行CLR绑定的类型
            types.Add(typeof(int));
            types.Add(typeof(float));
            types.Add(typeof(long));
            types.Add(typeof(object));
            types.Add(typeof(string));
            //types.Add(typeof(Dictionary<string, int>));
            //所有ILRuntime中的类型，实际上在C#运行时中都是ILRuntime.Runtime.Intepreter.ILTypeInstance的实例，
            //因此List<A> List<B>，如果A与B都是ILRuntime中的类型，只需要添加List<ILRuntime.Runtime.Intepreter.ILTypeInstance>即可
            //types.Add(typeof(Dictionary<ILRuntime.Runtime.Intepreter.ILTypeInstance, int>));
            //第二个参数为自动生成的代码保存在何处
            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types, BindingPath);
            AssetDatabase.Refresh();
        }
    }
}


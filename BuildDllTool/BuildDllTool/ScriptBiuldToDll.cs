using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BuildDllTool
{
    class ScriptBiuldToDll
    {
        public enum BuildStatus
        {
            Success = 0,
            Fail
        }

        static public BuildStatus Build(string unityAssetsPath, string dllPath, string unitySystemDllPath, string compilerDirectoryPath, string define)
        {
            //编译项目的base.dll
            Console.WriteLine("准备编译dll 10%");

            //清空dll的存放文件夹
            if (Directory.Exists(dllPath))
            {
                Directory.Delete(dllPath, true);
            }
            Directory.CreateDirectory(dllPath);

            //Unity 中存放脚本的文件
            string[] searchPath = new string[] { "Scripts", "ThridPartys" };
            for (int i = 0; i < searchPath.Length; i++)
            {
                searchPath[i] = Path.Combine(unityAssetsPath, searchPath[i]);
            }

            //找出所有的脚本
            List<string> files = new List<string>();
            foreach (var s in searchPath)
            {
                var fs = Directory.GetFiles(s, "*.*", SearchOption.AllDirectories).ToList();
                var _fs = fs.FindAll(f =>
                {
                    var _f = f.ToLower();
                    var exten = Path.GetExtension(_f);
                    if ((!_f.Contains("editor")) && (exten.Equals(".dll") || exten.Equals(".cs")))
                    {
                        return true;
                    }
                    return false;
                });

                files.AddRange(_fs);
            }

            files = files.Distinct().ToList();
            for (int i = 0; i < files.Count; i++)
            {
                files[i] = files[i].Replace('/', '\\').Trim('\\');
            }

            Console.WriteLine("开始整理script 20%");

            //项目中用到的dll
            var refDlls = files.FindAll(f => f.EndsWith(".dll"));
            //unity内脚本，用于先生成unity的dll文件，供hotfix.dll编译用
            var unityCs = files.FindAll(f => !f.EndsWith(".dll") && !f.Contains("@Hotfix"));
            //热更脚本，用于生成hotfix.dll
            var hotfixCs = files.FindAll(f => !f.EndsWith(".dll") && f.Contains("@Hotfix"));

            //临时目录
            var tempDirect = "d:/bd_temp";
            if (Directory.Exists(tempDirect))
            {
                Directory.Delete(tempDirect, true);
            }
            Directory.CreateDirectory(tempDirect);

            //除去不需要引用的dll
            for (int i = refDlls.Count - 1; i >= 0; i--)
            {
                var str = refDlls[i];
                if (str.Contains("Editor") || str.Contains("iOS") || str.Contains("Android") || str.Contains("StreamingAssets"))
                {
                    refDlls.RemoveAt(i);
                }
            }

            //拷贝dll到临时目录
            for (int i = 0; i < refDlls.Count; i++)
            {
                var copyto = Path.Combine(tempDirect, Path.GetFileName(refDlls[i]));
                File.Copy(refDlls[i], copyto, true);
                refDlls[i] = copyto;
            }

            //添加系统的dll
            refDlls.Add("System.dll");
            refDlls.Add("System.Core.dll");
            refDlls.Add("System.XML.dll");
            refDlls.Add("System.Data.dll");

            //添加Unity系统的dll
            string[] dllPaths = unitySystemDllPath.Split(',');
            foreach (string dll in dllPaths)
            {
                var dllfile = Directory.GetFiles(dll, "*.dll", SearchOption.AllDirectories);
                foreach (var d in dllfile)
                {
                    if (Path.GetFileNameWithoutExtension(d).StartsWith("Assembly-CSharp"))
                    {
                        continue;
                    }
                    refDlls.Add(d);
                }
            }

            var unityDllPath = dllPath + "unity.dll";

            Console.WriteLine("复制编译代码 30%");

            //拷贝非热更的cs文件到临时目录
            for (int i = 0; i < unityCs.Count; i++)
            {
                var copyto = Path.Combine(tempDirect, Path.GetFileName(unityCs[i]));
                int count = 1;
                while (File.Exists(copyto))
                {
                    //为解决mono.exe error: 文件名太长问题
                    copyto = copyto.Replace(".cs", "") + count + ".cs";
                    count++;
                }

                File.Copy(unityCs[i], copyto);
                unityCs[i] = copyto;
            }

            //检测dll，移除无效dll
            for (int i = refDlls.Count - 1; i >= 0; i--)
            {
                var r = refDlls[i];
                if (File.Exists(r))
                {
                    var fs = File.ReadAllBytes(r);
                    try
                    {
                        var assm = Assembly.Load(fs);
                    }
                    catch
                    {
                        Console.WriteLine("移除无效的 dll ：" + r);
                        refDlls.RemoveAt(i);
                    }
                }
            }

            Console.WriteLine("[1/2]开始编译 unity.dll 40%");

            BuildStatus unityResult = BuildStatus.Success;
            //编译 unity.dll
            try
            {
                unityResult = BuildDll(refDlls.ToArray(), unityCs.ToArray(), unityDllPath, compilerDirectoryPath, define);
            }
            catch (Exception e)
            {
                Console.WriteLine("unity.dll 编译失败：" + e);
                throw;
            }
            Console.WriteLine("[2/2]开始编译hotfix.dll 70%");

            //将unity.dll加入
            refDlls.Add(unityDllPath);

            //编译hotfix.dll
            var hotfixDllPath = dllPath + "hotfix.dll";
            BuildStatus hotfixResult = BuildStatus.Success;
            try
            {
                hotfixResult = BuildDll(refDlls.ToArray(), hotfixCs.ToArray(), hotfixDllPath, compilerDirectoryPath, define);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("清理临时文件 95%");
            Directory.Delete(tempDirect, true);

            if (unityResult == BuildStatus.Success && unityResult == hotfixResult)
            {
                Console.WriteLine("编译成功!");
                return BuildStatus.Success;
            }
            else
            {
                Console.WriteLine("编译失败!");
                return BuildStatus.Fail;
            }
        }

        /// <summary>
        /// 编译dll
        /// </summary>
        static public BuildStatus BuildDll(string[] refAssemblies, string[] codefiles, string output, string compilerDirectoryPath, string define)
        {
            // 设定编译参数,DLL代表需要引入的Assemblies
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = false;
            //在内存中生成
            cp.GenerateInMemory = true;
            //生成调试信息
            if (define.IndexOf("IL_DEBUG") >= 0)
            {
                cp.IncludeDebugInformation = true;
            }
            else
            {
                cp.IncludeDebugInformation = false;
            }

            //cp.TempFiles = new TempFileCollection(".", true);
            cp.OutputAssembly = output;
            //warning和 error分开,不然各种warning当成error,改死你
            cp.TreatWarningsAsErrors = false;
            cp.WarningLevel = 1;
            //编译选项
            cp.CompilerOptions = "-langversion:latest /optimize /unsafe /define:" + define;

            if (refAssemblies != null)
            {
                foreach (var d in refAssemblies)
                {
                    cp.ReferencedAssemblies.Add(d);
                }
            }

            // 编译代理
            CodeDomProvider provider;
            if (string.IsNullOrEmpty(compilerDirectoryPath))
            {
                provider = CodeDomProvider.CreateProvider("CSharp");
            }
            else
            {
                provider = CodeDomProvider.CreateProvider("cs", new Dictionary<string, string> {
                    { "CompilerDirectoryPath", compilerDirectoryPath }
                });
            }

            CompilerResults cr = provider.CompileAssemblyFromFile(cp, codefiles);
            if (true == cr.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(Environment.NewLine);
                }
                Console.WriteLine(sb);
            }
            else
            {
                return BuildStatus.Success;
            }
            return BuildStatus.Fail;
        }
    }
}

using System;
using System.Threading;

namespace BuildDllTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 5)
            {
                Console.WriteLine("Unity Asset 路径：" + args[0]);
                Console.WriteLine("dll 输出路径："+ args[1]);
                Console.WriteLine("Unity 系统的 dll 文件路径：" + args[2]);
                Console.WriteLine("编译配置路径：" + args[3]);
                Console.WriteLine("编译选项：" + args[4]);

                var result = ScriptBiuldToDll.Build(args[0], args[1], args[2], args[3], args[4]);

                Console.WriteLine("退出");
            }
            else
            {
                Console.WriteLine("参数不匹配！");
                Console.WriteLine("退出！");
            }
            Thread.Sleep(500);
            System.Diagnostics.Process.GetCurrentProcess().Close();
        }
    }
}

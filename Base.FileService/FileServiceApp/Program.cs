using Base.FileService;
using WeDonekRpc.Helper.Log;

namespace FileServiceApp
{
    internal class Program
    {
        private static void Main ( string[] args )
        {
            LogSystem.IsConsole = true;
            FileService.Init();
            Console.WriteLine("服务已启动");
            _ = Console.ReadLine();
        }
    }
}

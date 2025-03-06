using WeDonekRpc.Helper.Log;

namespace HrConsoleApp
{
    internal class Program
    {
        private static void Main ( string[] args )
        {
            LogSystem.IsConsole = true;
            Basic.HrService.HrService.InitService();
            Console.WriteLine("服务已启动");
            _ = Console.ReadLine();
        }
    }
}

using WeDonekRpc.Helper.Log;

namespace FormConsoleApp
{
    internal class Program
    {
        private static void Main ( string[] args )
        {
            LogSystem.IsConsole = true;
            Basic.FormService.FormService.InitService();
            Console.WriteLine("服务已启动");
            _ = Console.ReadLine();
        }
    }
}

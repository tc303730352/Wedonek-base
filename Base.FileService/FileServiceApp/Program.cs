using WeDonekRpc.ApiGateway;
using WeDonekRpc.Helper.Log;

namespace FileServiceApp
{
    internal class Program
    {
        private static void Main ( string[] args )
        {
            LogSystem.IsConsole = true;
            //全局
            GatewayServer.Global = new Global();
            //启动服务
            GatewayServer.InitApiService();
            Console.WriteLine("服务已启动");
            _ = Console.ReadLine();
        }
    }
}

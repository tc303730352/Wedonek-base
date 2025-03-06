using WeDonekRpc.ApiGateway;

namespace Base.FileService
{
    public class FileService
    {
        public static void Init ()
        {
            //全局
            GatewayServer.Global = new Global();
            //启动服务
            GatewayServer.InitApiService();
        }
    }
}
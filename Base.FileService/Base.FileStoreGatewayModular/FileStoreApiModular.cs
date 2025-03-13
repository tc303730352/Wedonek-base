using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;

namespace Base.FileStoreGatewayModular
{
    public class FileStoreApiModular : BasicApiModular
    {
        public FileStoreApiModular () : base("File_StoreGateway")
        {
        }
        protected override void Load ( IHttpGatewayOption option, IModularConfig config )
        {
            this.Config.ApiRouteFormat = "/file/{controller}/{name}";
        }

    }
}

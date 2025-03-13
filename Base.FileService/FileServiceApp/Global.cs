using Base.FileGateway;
using Base.FileStoreGatewayModular;
using WeDonekRpc.ApiGateway;
using WeDonekRpc.ApiGateway.Interface;
using WeDonekRpc.HttpApiDoc;

namespace FileServiceApp
{
    internal class Global : BasicGlobal
    {
        public override void Load ( IGatewayOption service )
        {
            service.RegDoc(new ApiDocModular());
            service.RegModular(new FileUpApiModular());
            service.RegModular(new FileStoreApiModular());
        }
    }
}

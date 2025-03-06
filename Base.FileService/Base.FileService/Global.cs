using WeDonekRpc.ApiGateway;
using WeDonekRpc.ApiGateway.Interface;
using WeDonekRpc.HttpApiDoc;

namespace Base.FileService
{
    internal class Global : BasicGlobal
    {
        public override void Load (IGatewayOption service)
        {
            service.RegDoc(new ApiDocModular());
            service.RegModular(new FileApiModular());
        }
    }
}

using Basic.HrGatewaryModular.ExtendService;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;
namespace Basic.HrGatewaryModular
{
    internal class HrGatewayModular : IRpcInitModular
    {
        public void Init (IIocService ioc)
        {

        }

        public void InitEnd (IIocService ioc, IRpcService service)
        {
            EnumService.Load();
        }

        public void Load (RpcInitOption option)
        {

        }
    }
}

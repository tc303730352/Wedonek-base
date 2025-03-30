using Basic.HrGatewaryModular.ExtendService;
using Basic.HrGatewaryModular.OpLog;
using WeDonekRpc.CacheModular;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;
namespace Basic.HrGatewaryModular
{
    internal class HrGatewayModular : IRpcInitModular
    {
        public void Init ( IIocService ioc )
        {

        }

        public void InitEnd ( IIocService ioc, IRpcService service )
        {
            EnumService.Load();
            service.StartUpComplating += this.Service_StartUpComplating;
        }

        private void Service_StartUpComplating ()
        {
            OperateLogService.Init();
        }

        public void Load ( RpcInitOption option )
        {
            option.LoadModular<CacheModular>();
        }
    }
}

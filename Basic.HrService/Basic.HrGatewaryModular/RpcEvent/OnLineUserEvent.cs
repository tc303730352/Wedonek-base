using Basic.HrGatewaryModular.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrGatewaryModular.RpcEvent
{
    internal class OnlineUserEvent : IRpcApiService
    {
        private readonly IUserOnlineService _Service;

        public OnlineUserEvent ( IUserOnlineService service )
        {
            this._Service = service;
        }
        public void ClearOnlineUser ()
        {
            this._Service.ClearOnlineUser();
        }
    }
}

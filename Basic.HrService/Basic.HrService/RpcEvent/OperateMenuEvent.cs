using Basic.HrRemoteModel.OpMenu.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class OperateMenuEvent : IRpcApiService
    {
        private readonly IOperateMenuService _Service;

        public OperateMenuEvent ( IOperateMenuService service )
        {
            this._Service = service;
        }
        public OperateMenu[] GetOperateMenus ()
        {
            return this._Service.GetMenus();
        }
    }
}

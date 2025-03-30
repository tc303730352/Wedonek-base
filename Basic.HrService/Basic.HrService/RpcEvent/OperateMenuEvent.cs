using Basic.HrRemoteModel.OpMenu;
using Basic.HrRemoteModel.OpMenu.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
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
        public PagingResult<OperateMenuDto> QueryOperateMenu ( QueryOperateMenu obj )
        {
            return this._Service.Query(obj.Query, obj.ToBasicPage());
        }
        public long AddOperateMenu ( AddOperateMenu obj )
        {
            return this._Service.Add(obj.Datum);
        }
        public void DeleteOperateMenu ( DeleteOperateMenu obj )
        {
            this._Service.Delete(obj.Id);
        }
        public OperateMenuDto GetOperateMenu ( GetOperateMenu obj )
        {
            return this._Service.Get(obj.Id);
        }
        public bool SetOperateMenuState ( SetOperateMenuState obj )
        {
            return this._Service.SetIsEnable(obj.Id, obj.IsEnable);
        }
        public OperateMenu[] GetOperateMenus ()
        {
            return this._Service.GetMenus();
        }
    }
}

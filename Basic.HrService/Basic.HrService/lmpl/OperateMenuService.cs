using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpMenu.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class OperateMenuService : IOperateMenuService
    {
        private readonly IOperateMenuCollect _Service;

        public OperateMenuService ( IOperateMenuCollect service )
        {
            this._Service = service;
        }

        public long Add ( OpMenuAdd add )
        {
            return this._Service.Add(add);
        }
        public bool SetIsEnable ( long id, bool isEnable )
        {
            DBOperateMenu menu = this._Service.Get(id);
            return this._Service.SetIsEnable(menu, isEnable);
        }
        public OperateMenu[] GetMenus ()
        {
            return this._Service.GetMenus();
        }

        public void Delete ( long id )
        {
            DBOperateMenu menu = this._Service.Get(id);
            this._Service.Delete(menu);
        }

        public OperateMenuDto Get ( long id )
        {
            DBOperateMenu menu = this._Service.Get(id);
            return menu.ConvertMap<DBOperateMenu, OperateMenuDto>();
        }

        public PagingResult<OperateMenuDto> Query ( OpMenuQuery query, IBasicPage basicPage )
        {
            OperateMenuDto[] list = this._Service.Query<OperateMenuDto>(query, basicPage, out int count);
            return new PagingResult<OperateMenuDto>(list, count);
        }
    }
}

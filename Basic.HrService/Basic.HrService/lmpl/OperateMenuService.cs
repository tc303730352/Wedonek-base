using Basic.HrCollect;
using Basic.HrRemoteModel.OpMenu.Model;
using Basic.HrService.Interface;

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

        public OperateMenu[] GetMenus ()
        {
            return this._Service.GetMenus();
        }
    }
}

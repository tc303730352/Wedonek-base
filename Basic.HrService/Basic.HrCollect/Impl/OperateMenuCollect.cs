using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;

namespace Basic.HrCollect.Impl
{
    internal class OperateMenuCollect : IOperateMenuCollect
    {
        private readonly IOperateMenuDAL _OpMenu;

        public OperateMenuCollect ( IOperateMenuDAL opMenu )
        {
            this._OpMenu = opMenu;
        }

        public OperateMenu[] GetMenus ()
        {
            return this._OpMenu.Gets<OperateMenu>(a => a.IsEnable);
        }

        public long Add ( OpMenuAdd add )
        {
            DBOperateMenu menu = add.ConvertMap<OpMenuAdd, DBOperateMenu>();
            return this._OpMenu.Add(menu);
        }
    }
}

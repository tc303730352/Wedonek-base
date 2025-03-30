using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class OperateMenuCollect : IOperateMenuCollect
    {
        private readonly IOperateMenuDAL _OpMenu;

        public OperateMenuCollect ( IOperateMenuDAL opMenu )
        {
            this._OpMenu = opMenu;
        }
        public DBOperateMenu Get ( long id )
        {
            return this._OpMenu.Get(id);
        }
        public OperateMenu[] GetMenus ()
        {
            return this._OpMenu.Gets<OperateMenu>(a => a.IsEnable);
        }
        public Result[] Query<Result> ( OpMenuQuery query, IBasicPage paging, out int count ) where Result : class
        {
            return this._OpMenu.Query<Result>(query, paging, out count);
        }
        public bool SetIsEnable ( DBOperateMenu menu, bool isEnable )
        {
            if ( menu.IsEnable == isEnable )
            {
                return false;
            }
            this._OpMenu.SetIsEnable(menu, isEnable);
            return true;
        }
        public long Add ( OpMenuAdd add )
        {
            if ( this._OpMenu.IsExists(c => c.Title == add.Title) )
            {
                throw new ErrorException("hr.operate.title.repeat");
            }
            else if ( this._OpMenu.IsExists(c => c.RoutePath == add.RoutePath) )
            {
                throw new ErrorException("hr.operate.route.path.repeat");
            }
            DBOperateMenu menu = add.ConvertMap<OpMenuAdd, DBOperateMenu>();
            return this._OpMenu.Add(menu);
        }

        public void Delete ( DBOperateMenu menu )
        {
            if ( menu.IsEnable )
            {
                throw new ErrorException("hr.operate.menu.already.enable");
            }
            this._OpMenu.Delete(menu.Id);
        }
    }
}

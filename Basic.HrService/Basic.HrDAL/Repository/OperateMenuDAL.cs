using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class OperateMenuDAL : BasicDAL<DBOperateMenu, long>, IOperateMenuDAL
    {
        public OperateMenuDAL ( IRepository<DBOperateMenu> basicDAL ) : base(basicDAL)
        {
        }

        public long Add ( DBOperateMenu add )
        {
            add.Id = IdentityHelper.CreateId();
            add.IsEnable = true;
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        public Result[] Query<Result> ( OpMenuQuery query, IBasicPage paging, out int count ) where Result : class
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }

        public void SetIsEnable ( DBOperateMenu menu, bool isEnable )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == isEnable, a => a.Id == menu.Id) )
            {
                throw new ErrorException("hr.operate.menu.set.fail");
            }
        }
    }
}

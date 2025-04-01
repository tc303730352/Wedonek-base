using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class CustomControlDAL : BasicDAL<DBCustomControl, long>, ICustomControlDAL
    {
        public CustomControlDAL ( IRepository<DBCustomControl> basicDAL ) : base(basicDAL)
        {
        }

        public long Add ( ControlAdd data )
        {
            DBCustomControl add = data.ConvertMap<ControlAdd, DBCustomControl>();
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        public Result[] Query<Result> ( ControlQuery query, IBasicPage paging, out int count ) where Result : class
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }

        public void SetStatus ( long id, ControlStatus status )
        {
            if ( !this._BasicDAL.Update(a => a.Status == status, a => a.Id == id) )
            {
                throw new ErrorException("form.control.set.fail");
            }
        }
    }
}

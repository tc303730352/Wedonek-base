using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Base.FileDAL.Repository
{
    internal class UserFileDirDAL : BasicDAL<DBUserFileDir, long>, IUserFileDirDAL
    {
        public UserFileDirDAL ( IRepository<DBUserFileDir> basicDAL ) : base(basicDAL)
        {
        }
        public Result GetByKey<Result> ( string key ) where Result : class, new()
        {
            return this._BasicDAL.Get<Result>(a => a.DirKey == key);
        }
        public Result[] Query<Result> ( UserFileDirQuery query, IBasicPage paging, out int count ) where Result : class, new()
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
        public long[] GetDirId ( string[] dirKey )
        {
            return this._BasicDAL.Gets(a => dirKey.Contains(a.DirKey), a => a.Id);
        }
        public long Add ( DBUserFileDir add )
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
    }
}

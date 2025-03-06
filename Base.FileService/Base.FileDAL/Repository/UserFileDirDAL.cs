using Base.FileModel.DB;
using WeDonekRpc.SqlSugar;

namespace Base.FileDAL.Repository
{
    internal class UserFileDirDAL : BasicDAL<DBUserFileDir, long>, IUserFileDirDAL
    {
        public UserFileDirDAL (IRepository<DBUserFileDir> basicDAL) : base(basicDAL)
        {
        }
        public Result GetByKey<Result> (string key) where Result : class, new()
        {
            return this._BasicDAL.Get<Result>(a => a.DirKey == key);
        }

        public long[] GetDirId (string[] dirKey)
        {
            return this._BasicDAL.Gets(a => dirKey.Contains(a.DirKey), a => a.Id);
        }
    }
}

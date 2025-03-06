using Base.FileModel.DB;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Base.FileDAL.Repository
{
    internal class FileDAL : BasicDAL<DBFileList, long>, IFileDAL
    {
        public FileDAL (IRepository<DBFileList> basicDAL) : base(basicDAL)
        {
        }
        public void Add (DBFileList file)
        {
            file.Id = IdentityHelper.CreateId();
            file.SaveTime = DateTime.Now;
            this._BasicDAL.Insert(file);
        }
        public long FindFileId (string md5)
        {
            return this._BasicDAL.Get(a => a.FileMd5 == md5, a => a.Id);
        }
    }
}

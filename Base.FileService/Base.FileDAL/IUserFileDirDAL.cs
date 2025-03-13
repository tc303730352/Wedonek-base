using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using WeDonekRpc.Model;

namespace Base.FileDAL
{
    public interface IUserFileDirDAL : IBasicDAL<DBUserFileDir, long>
    {
        Result[] Query<Result> ( UserFileDirQuery query, IBasicPage paging, out int count ) where Result : class, new();
        long Add ( DBUserFileDir add );
        Result GetByKey<Result> ( string key ) where Result : class, new();
        long[] GetDirId ( string[] dirKey );
    }
}
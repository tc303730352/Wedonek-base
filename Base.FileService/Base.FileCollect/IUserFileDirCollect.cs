using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using WeDonekRpc.Model;

namespace Base.FileCollect
{
    public interface IUserFileDirCollect
    {
        bool Set ( DBUserFileDir source, UserFileDirSet set );
        long Add ( UserFileDirAdd add );
        Result Get<Result> ( long id ) where Result : class, new();
        Result[] GetAll<Result> () where Result : class, new();
        UserFileDirDto GetDir ( string dirKey );
        long[] GetDirId ( string[] dirKey );
        void Delete ( DBUserFileDir dir );

        Result[] Query<Result> ( UserFileDirQuery query, IBasicPage paging, out int count ) where Result : class, new();
    }
}
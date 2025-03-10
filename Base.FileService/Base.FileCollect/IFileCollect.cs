using Base.FileModel.BaseFile;
using WeDonekRpc.Model;

namespace Base.FileCollect
{
    public interface IFileCollect
    {
        Result[] Query<Result> ( FileQuery query, IBasicPage paging, out int count ) where Result : class;
        long Add ( FileDatum add );
        long FindFileId ( string md5 );
        Result Get<Result> ( long fileId ) where Result : class;
    }
}
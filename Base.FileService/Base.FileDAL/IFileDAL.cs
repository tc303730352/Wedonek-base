using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using WeDonekRpc.Model;

namespace Base.FileDAL
{
    public interface IFileDAL : IBasicDAL<DBFileList, long>
    {
        void Add ( DBFileList file );
        long FindFileId ( string md5 );
        Result[] Query<Result> ( FileQuery query, IBasicPage paging, out int count ) where Result : class;
    }
}
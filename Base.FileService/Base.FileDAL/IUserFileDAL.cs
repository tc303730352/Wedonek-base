using Base.FileModel.DB;
using Base.FileModel.UserFile;
using WeDonekRpc.Model;

namespace Base.FileDAL
{
    public interface IUserFileDAL : IBasicDAL<DBUserFileList, long>
    {
        Result[] Query<Result> ( UserFileQuery query, IBasicPage paging, out int count ) where Result : class, new();
        UserFileBasic[] GetFiles ( long dirId, long linkBizPk, string tag );
        void Add ( DBUserFileList add );
        void Drop ( long[] ids );
        void Drop ( long id );
        void Delete ( long id );
        UserFileDto GetFile ( long id );
        long[] GetIds ( long dirId, long linkBizPk, string tag );
        void SaveFile ( long[] fileId, long linkBizPk, long[] dropId );
        void SaveFile ( long fileId, long linkBizPk, long[] dropId );
        long[] GetIds ( long[] dirId, long[] linkBikzPk );
        void SaveFile ( Dictionary<long, int> sort );
        Dictionary<long, int> GetFileNum ( long[] fileId );
        Dictionary<long, int> GetDirFileNum ( long[] dirId );
    }
}
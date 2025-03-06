using Base.FileModel.DB;
using Base.FileModel.UserFile;

namespace Base.FileDAL
{
    public interface IUserFileDAL : IBasicDAL<DBUserFileList, long>
    {
        UserFileBasic[] GetFiles (long dirId, long linkBizPk, string tag);
        void Add (DBUserFileList add);
        void Drop (long[] ids);
        void Drop (long id);
        void Delete (long id);
        UserFileDto GetFile (long id);
        long[] GetIds (long dirId, long linkBizPk, string tag);
        void SaveFile (long[] fileId, long linkBizPk, long[] dropId);
        void SaveFile (long fileId, long linkBizPk, long[] dropId);
        long[] GetIds (long[] dirId, long[] linkBikzPk);
        void SaveFile (Dictionary<long, int> sort);
    }
}
using Base.FileModel.DB;
using Base.FileModel.UserFile;

namespace Base.FileCollect
{
    public interface IUserFileCollect
    {
        UserFileBasic[] GetFiles (long dirId, long linkBizPk, string tag);
        UserFileDto GetFile (long id);
        DBUserFileList Add (UserFileAdd add);
        void SaveFile (long[] fileId, long linkBizPk, long[] dropId);
        void SaveFile (UserFileDto file, long linkBizPk, long[] dropId);
        void SaveSort (Dictionary<long, int> sort);
        long[] GetIds (long dirId, long linkBikzPk, string tag);
        long[] GetIds (long[] dirId, long[] linkBikzPk);
        void Drop (long[] ids);
        void Delete (long id);
        void Drop (long id);
    }
}
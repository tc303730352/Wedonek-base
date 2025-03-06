using Base.FileModel.DB;

namespace Base.FileDAL
{
    public interface IFileDAL : IBasicDAL<DBFileList, long>
    {
        void Add (DBFileList file);
        long FindFileId (string md5);
    }
}
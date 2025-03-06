using Base.FileModel.DB;

namespace Base.FileDAL
{
    public interface IUserFileDirDAL : IBasicDAL<DBUserFileDir, long>
    {
        Result GetByKey<Result> (string key) where Result : class, new();
        long[] GetDirId (string[] dirKey);
    }
}
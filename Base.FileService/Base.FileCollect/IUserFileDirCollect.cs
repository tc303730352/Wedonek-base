using Base.FileModel.UserFileDir;

namespace Base.FileCollect
{
    public interface IUserFileDirCollect
    {
        Result[] GetAll<Result> () where Result : class, new();
        UserFileDirDto GetDir ( string dirKey );
        long[] GetDirId ( string[] dirKey );
    }
}
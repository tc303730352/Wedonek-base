using Base.FileModel.UserFileDir;

namespace Base.FileCollect
{
    public interface IUserFileDirCollect
    {
        UserFileDirDto GetDir (string dirKey);
        long[] GetDirId (string[] dirKey);
    }
}
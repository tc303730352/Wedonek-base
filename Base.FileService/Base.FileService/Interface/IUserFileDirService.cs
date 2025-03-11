using Base.FileModel.UserFileDir;

namespace Base.FileService.Interface
{
    public interface IUserFileDirService
    {
        UserFileDirBase[] GetDirs ();
    }
}
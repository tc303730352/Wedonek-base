using Base.FileModel.UserFileDir;

namespace Base.FileStoreGatewayModular.Interface
{
    public interface IUserFileDirService
    {
        UserFileDirBase[] GetDirs ();
    }
}
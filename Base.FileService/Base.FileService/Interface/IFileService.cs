using Base.FileService.Model;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.Interface
{
    public interface IFileService
    {
        UserFile SaveFile (IUpFile file, UpArg arg);
    }
}
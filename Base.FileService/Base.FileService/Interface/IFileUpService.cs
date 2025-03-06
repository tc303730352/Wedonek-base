using Base.FileService.Model;
using WeDonekRpc.Modular;

namespace Base.FileService.Interface
{
    public interface IFilePreUpService
    {
        PreUpFileResult SaveFile (PreUpFile file, string dirKey, IUserState state);
    }
}
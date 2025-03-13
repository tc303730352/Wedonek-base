using Base.FileGateway.Model;
using WeDonekRpc.Modular;

namespace Base.FileGateway.Interface
{
    public interface IFilePreUpService
    {
        PreUpFileResult SaveFile (PreUpFile file, string dirKey, IUserState state);
    }
}
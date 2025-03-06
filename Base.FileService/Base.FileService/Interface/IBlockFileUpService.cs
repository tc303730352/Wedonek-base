using Base.FileService.FileBlockUp.Model;
using WeDonekRpc.HttpApiGateway.FileUp.Interface;
using WeDonekRpc.HttpService.Interface;
using WeDonekRpc.Modular;

namespace Base.FileService.lmpl
{
    public interface IBlockFileUpService
    {
        void Init ( IBlockUp<BlockUpArg> task, IUserState state );
        void SaveFile ( IUpFileResult result, IUpFile file );
    }
}
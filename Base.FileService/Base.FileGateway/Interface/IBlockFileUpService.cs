using Base.FileGateway.FileBlockUp.Model;
using WeDonekRpc.HttpApiGateway.FileUp.Interface;
using WeDonekRpc.HttpService.Interface;
using WeDonekRpc.Modular;

namespace Base.FileGateway.Interface
{
    public interface IBlockFileUpService
    {
        void Init ( IBlockUp<BlockUpArg> task, IUserState state );
        void SaveFile ( IUpFileResult result, IUpFile file );
    }
}
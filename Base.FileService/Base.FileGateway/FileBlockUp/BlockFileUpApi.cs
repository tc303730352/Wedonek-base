using Base.FileGateway.FileBlockUp.Model;
using Base.FileGateway.Interface;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.FileUp.Interface;
using WeDonekRpc.HttpApiGateway.FileUp.Model;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileGateway.FileBlockUp
{
    [ApiRouteName("/file/block/up")]
    internal class BlockFileUpApi : UpBlockFileTask<BlockUpArg>
    {
        private readonly IBlockFileUpService _Service;

        public BlockFileUpApi ( IBlockFileUpService service )
        {
            this._Service = service;
        }
        public override void InitTask ( IApiService service, IBlockUp<BlockUpArg> task )
        {
            this._Service.Init(task, service.UserState);
        }
        public override void Complete ( IUpFileResult result, IUpFile upFile )
        {
            this._Service.SaveFile(result, upFile);
        }

        public override void UpFail ( UpBasicFile file, string error )
        {
        }
    }
}

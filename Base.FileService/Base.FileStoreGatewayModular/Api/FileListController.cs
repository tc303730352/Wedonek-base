using Base.FileModel.BaseFile;
using Base.FileStoreGatewayModular.Interface;
using Base.FileStoreGatewayModular.Model.File;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileStoreGatewayModular.Api
{
    internal class FileListApi : ApiController
    {
        private readonly IFileListService _Service;

        public FileListApi ( IFileListService service )
        {
            this._Service = service;
        }

        public PagingResult<FileUIDto> Query ( PagingParam<FileQuery> query )
        {
            return this._Service.Query(query);
        }
    }
}

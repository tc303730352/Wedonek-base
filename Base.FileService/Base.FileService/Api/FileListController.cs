using Base.FileModel.BaseFile;
using Base.FileService.Interface;
using Base.FileService.Model.File;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileService.Api
{
    internal class FileListController : ApiController
    {
        private readonly IFileListService _Service;

        public FileListController ( IFileListService service )
        {
            this._Service = service;
        }

        public PagingResult<FileUIDto> Query ( PagingParam<FileQuery> query )
        {
            return this._Service.Query(query);
        }
    }
}

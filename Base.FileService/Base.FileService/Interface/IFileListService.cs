using Base.FileModel.BaseFile;
using Base.FileService.Model.File;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileService.Interface
{
    public interface IFileListService
    {
        PagingResult<FileUIDto> Query ( PagingParam<FileQuery> query );
    }
}
using Base.FileModel.BaseFile;
using Base.FileStoreGatewayModular.Model.File;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileStoreGatewayModular.Interface
{
    public interface IFileListService
    {
        PagingResult<FileUIDto> Query ( PagingParam<FileQuery> query );
    }
}
using Base.FileModel.BaseFile;
using Base.FileService.Interface;
using Base.FileStoreGatewayModular.VisitEvent;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpApiGateway.Response;

namespace Base.FileStoreGatewayModular.Api
{
    [ApiEventAttr(typeof(FileVisitEvent))]
    internal class VideoFileController : ApiController
    {
        private readonly IVideoFileService _Service;

        public VideoFileController ( IVideoFileService service )
        {
            this._Service = service;
        }

        [ApiPower(false)]
        [ApiRouteName("/file/video/thumbnail/")]
        public IResponse ReadThumbnail ()
        {
            FileBase file = this.RequestState.Get<FileBase>("file");
            FileInfo fileInfo = this._Service.GetThumbnail(file);
            return new StreamResponse(fileInfo);
        }
    }
}

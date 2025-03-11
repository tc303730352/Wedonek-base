using Base.FileModel.UserFile;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.VisitEvent;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Img;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;

namespace Base.FileService.Api
{
    [ApiEventAttr(typeof(UserFileVisitEvent))]
    internal class ImageFileController : ApiController
    {
        private readonly IImageFileService _ImgService;

        public ImageFileController ( IImageFileService imgService )
        {
            this._ImgService = imgService;
        }
        [ApiPower(false)]
        [ApiRouteName("/file/user/read/image/")]
        public IResponse ReadImage ( bool isDown )
        {
            UserFileDto file = base.RequestState.Get<UserFileDto>("file");
            ImgOperate op = this.Request.QueryString.GetImgOperate();
            return this._ImgService.ReadImage(file, isDown, op);
        }
    }
}

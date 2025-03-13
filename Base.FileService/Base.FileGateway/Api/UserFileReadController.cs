using Base.FileGateway.VisitEvent;
using Base.FileModel.UserFile;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpApiGateway.Response;

namespace Base.FileGateway.Api
{
    [ApiEventAttr(typeof(UserFileVisitEvent))]
    internal class UserFileReadController : ApiController
    {
        [ApiPower(false)]
        [ApiRouteName("/file/user/read/")]
        public IResponse ReadFile ( bool isDown )
        {
            UserFileDto file = RequestState.Get<UserFileDto>("file");
            FileInfo fileInfo = new FileInfo(file.FilePath);
            if ( isDown )
            {
                return new StreamResponse(fileInfo, file.FileName) { IsBinary = true };
            }
            return new StreamResponse(fileInfo);
        }
    }
}

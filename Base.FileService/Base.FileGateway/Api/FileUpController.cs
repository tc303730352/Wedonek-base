using Base.FileGateway.FileCheck;
using Base.FileGateway.Helper;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway;

namespace Base.FileGateway.Api
{
    [ApiUpConfig(typeof(UpFileCheck))]
    internal class FileUpController : ApiController
    {
        private readonly IFileService _Service;

        public FileUpController ( IFileService service )
        {
            this._Service = service;
        }

        [ApiRouteName("/file/up/{configKey}")]
        public UserFile UpFile ( long? linkBizPk, string tag )
        {
            if ( this.Request.Files == null || this.Request.Files.Count == 0 )
            {
                throw new ErrorException("file.up.null");
            }
            return this._Service.SaveFile(this.Request.Files[0], new UpArg
            {
                Dir = UpFileCheck.SaveDir,
                UserDir = UpFileCheck.UserDir,
                UserId = this.UserState.ToUserId(),
                UserType = this.UserState.ToUserType(),
                LinkBizPk = linkBizPk,
                Tag = tag
            });
        }
    }
}

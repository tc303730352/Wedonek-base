using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.HttpApiGateway;

namespace Base.FileService.Api
{
    internal class PreFileUpController : ApiController
    {
        private readonly IFilePreUpService _Service;

        public PreFileUpController (IFilePreUpService service)
        {
            this._Service = service;
        }

        [ApiRouteName("/file/preup/{configkey}")]
        public PreUpFileResult PreUpFile (PreUpFile obj, [ApiPathParam("configkey")] string configKey)
        {
            return this._Service.SaveFile(obj, configKey, base.UserState);
        }
    }
}

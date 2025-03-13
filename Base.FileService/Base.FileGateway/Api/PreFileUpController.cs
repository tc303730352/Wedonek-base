using Base.FileGateway.Interface;
using Base.FileGateway.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.HttpApiGateway;

namespace Base.FileGateway.Api
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
            return this._Service.SaveFile(obj, configKey, UserState);
        }
    }
}

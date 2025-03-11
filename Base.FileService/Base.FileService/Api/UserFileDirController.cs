using Base.FileModel.UserFileDir;
using Base.FileService.Interface;
using WeDonekRpc.HttpApiGateway;

namespace Base.FileService.Api
{
    internal class UserFileDirController : ApiController
    {
        private readonly IUserFileDirService _Service;

        public UserFileDirController ( IUserFileDirService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取用户目录列表
        /// </summary>
        /// <returns></returns>
        public UserFileDirBase[] GetDirs ()
        {
            return this._Service.GetDirs();
        }
    }
}

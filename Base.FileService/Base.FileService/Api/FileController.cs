using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
namespace Base.FileService.Api
{
    internal class FileController : ApiController
    {
        private readonly IUserFileService _Service;

        public FileController (IUserFileService service)
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取已上传的文件和上传配置
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [ApiRouteName("/file/config")]
        public DirUpConfig GetUploadConfig (GetConfigArg arg)
        {
            return this._Service.GetUploadConfig(arg);
        }
        [ApiRouteName("/file/delete")]
        public void DeleteFile ([NumValidate("file.id.error", 1)] long fileId)
        {
            this._Service.Delete(fileId, base.UserState);
        }
        /// <summary>
        /// 保存文件排列顺序
        /// </summary>
        /// <param name="sort"></param>
        [ApiRouteName("/file/sort")]
        public void SaveSort (SetSort sort)
        {
            this._Service.SaveSort(sort.Sort);
        }
    }
}

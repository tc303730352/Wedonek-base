using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using Base.FileStoreGatewayModular.Interface;
using Base.FileStoreGatewayModular.Model.UserDir;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileStoreGatewayModular.Api
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
        /// <summary>
        /// 获取用户文件目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DBUserFileDir Get ( [NumValidate("file.user.dir.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }
        /// <summary>
        /// 查询用户文件目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagingResult<UserDirDto> Query ( PagingParam<UserFileDirQuery> param )
        {
            return this._Service.Query(param);
        }
        /// <summary>
        /// 删除用户文件目录
        /// </summary>
        /// <param name="id"></param>
        public void Delete ( [NumValidate("file.user.dir.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }
        /// <summary>
        /// 添加用户文件目录
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public long Add ( UserFileDirAdd add )
        {
            return this._Service.Add(add);
        }
        /// <summary>
        /// 修改用户文件目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Set ( LongParam<UserFileDirSet> param )
        {
            return this._Service.Set(param.Id, param.Value);
        }
    }
}

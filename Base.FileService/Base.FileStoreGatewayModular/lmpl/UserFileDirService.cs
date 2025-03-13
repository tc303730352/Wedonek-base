using Base.FileCollect;
using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using Base.FileStoreGatewayModular.Interface;
using Base.FileStoreGatewayModular.Model.UserDir;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileStoreGatewayModular.lmpl
{
    internal class UserFileDirService : IUserFileDirService
    {
        private readonly IUserFileDirCollect _Service;
        private readonly IUserFileCollect _UserFile;

        public UserFileDirService ( IUserFileDirCollect service, IUserFileCollect userFile )
        {
            this._UserFile = userFile;
            this._Service = service;
        }
        public PagingResult<UserDirDto> Query ( PagingParam<UserFileDirQuery> param )
        {
            DBUserFileDir[] list = this._Service.Query<DBUserFileDir>(param.Query, param.ToBasicPaging(), out int count);
            if ( list.IsNull() )
            {
                return new PagingResult<UserDirDto>();
            }
            UserDirDto[] dirs = list.ConvertMap<DBUserFileDir, UserDirDto>();
            Dictionary<long, int> fileNum = this._UserFile.GetDirFileNum(list.ConvertAll(c => c.Id));
            dirs.ForEach(c =>
            {
                c.FileNum = fileNum.GetValueOrDefault(c.Id);
            });
            return new PagingResult<UserDirDto>(dirs, count);
        }
        public long Add ( UserFileDirAdd add )
        {
            return this._Service.Add(add);
        }

        public void Delete ( long id )
        {
            if ( !this._UserFile.CheckIsNullDir(id) )
            {
                throw new ErrorException("file.user.dir.not.null");
            }
            DBUserFileDir dir = this._Service.Get<DBUserFileDir>(id);
            this._Service.Delete(dir);
        }

        public DBUserFileDir Get ( long id )
        {
            return this._Service.Get<DBUserFileDir>(id);
        }

        public UserFileDirBase[] GetDirs ()
        {
            return this._Service.GetAll<UserFileDirBase>();
        }

        public bool Set ( long id, UserFileDirSet data )
        {
            DBUserFileDir dir = this._Service.Get<DBUserFileDir>(id);
            return this._Service.Set(dir, data);
        }
    }
}

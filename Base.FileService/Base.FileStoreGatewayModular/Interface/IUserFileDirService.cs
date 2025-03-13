using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using Base.FileStoreGatewayModular.Model.UserDir;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileStoreGatewayModular.Interface
{
    public interface IUserFileDirService
    {
        PagingResult<UserDirDto> Query ( PagingParam<UserFileDirQuery> param );
        long Add ( UserFileDirAdd add );
        void Delete ( long id );
        DBUserFileDir Get ( long id );
        UserFileDirBase[] GetDirs ();
        bool Set ( long id, UserFileDirSet value );
    }
}
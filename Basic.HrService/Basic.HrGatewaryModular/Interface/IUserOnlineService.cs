using Basic.HrGatewaryModular.Model;
using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IUserOnlineService
    {
        void ClearOnlineUser ();
        void Add ( LoginResult res, LoginState state, string accreditId );
        void KickOut ( string accreditId );
        PagingResult<OnlineUser> Query ( IBasicPage paging );
    }
}
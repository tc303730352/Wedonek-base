using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface ILoginUserQueryService
    {
        PagingResult<LoginUserDatum> Query (LoginUserQuery query, IBasicPage paging);
    }
}
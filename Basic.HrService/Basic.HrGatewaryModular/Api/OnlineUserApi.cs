using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Api
{
    /// <summary>
    /// 在线用户列表
    /// </summary>
    internal class OnlineUserApi : ApiController
    {
        private readonly IUserOnlineService _Service;

        public OnlineUserApi ( IUserOnlineService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 查询在线用户
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public PagingResult<OnlineUser> Query ( BasicPage paging )
        {
            return this._Service.Query(paging);
        }
        /// <summary>
        /// 踢出用户
        /// </summary>
        /// <param name="accreditId"></param>
        public void KickOut ( [NullValidate("hr.accredit.id.null")][LenValidate("hr.accredit.id.error", 32)] string accreditId )
        {
            this._Service.KickOut(accreditId);
        }
    }
}

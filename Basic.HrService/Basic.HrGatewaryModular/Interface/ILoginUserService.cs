using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface ILoginUserService
    {
        void UpdatePwd ( long empId, PwdSetArg set );
        void ResetPwd ( long empId );
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="empId">人员ID</param>
        void DeleteAccount ( long empId );

        /// <summary>
        /// 开通账号
        /// </summary>
        void OpenAccount ( long[] empId, long companyId );

        /// <summary>
        /// 查询登陆账号信息
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <param name="paging">分页参数</param>
        /// <returns>登陆用户资料</returns>
        PagingResult<LoginUserDatum> QueryLoginUser ( LoginUserQuery query, IBasicPage paging );
    }
}

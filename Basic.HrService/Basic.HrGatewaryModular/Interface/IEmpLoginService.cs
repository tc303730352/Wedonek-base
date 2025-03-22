using Basic.HrGatewaryModular.Model.EmpLogin;
using WeDonekRpc.Modular;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IEmpLoginService
    {
        /// <summary>
        /// 切换公司
        /// </summary>
        /// <param name="state"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        LoginDatum Switch ( IUserState state, long companyId );
        LoginDatum GetLoginDatum ( IUserState state );
        /// <summary>
        /// 人员密码登陆
        /// </summary>
        /// <param name="loginName">登陆名</param>
        /// <param name="password">登陆密码</param>
        /// <returns>登陆结果</returns>
        EmpLoginRes EmpPwdLogin ( string loginName, string password );

    }
}

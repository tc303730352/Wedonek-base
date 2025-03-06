using Basic.HrGatewaryModular.Model.EmpLogin;
using WeDonekRpc.Modular;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IEmpLoginService
    {
        LoginDatum GetLoginDatum (IUserState state);
        /// <summary>
        /// 人员密码登陆
        /// </summary>
        /// <param name="loginName">登陆名</param>
        /// <param name="password">登陆密码</param>
        /// <returns>登陆结果</returns>
        EmpLoginRes EmpPwdLogin (string loginName, string password);

    }
}

using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.EmpLogin;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class EmpLoginApi : ApiController
    {
        private readonly IEmpLoginService _Service;
        public EmpLoginApi ( IEmpLoginService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public string[] CheckPower ( [NullValidate("hr.power.null")] string[] power )
        {
            if ( this.UserState.IsAdmin() )
            {
                return power;
            }
            return base.UserState.Power.Intersect(power).ToArray();
        }
        /// <summary>
        /// 获取登陆资料
        /// </summary>
        /// <returns></returns>
        public LoginDatum GetLoginDatum ()
        {
            return this._Service.GetLoginDatum(base.UserState);
        }
        /// <summary>
        /// 切换公司
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public LoginDatum Switch ( [NumValidate("hr.company.id.error", 1)] long companyId )
        {
            return this._Service.Switch(base.UserState, companyId);
        }
        /// <summary>
        /// 人员密码登陆
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>登陆结果</returns>
        [ApiPower(false)]
        public EmpLoginRes EmpPwdLogin ( [NullValidate("hr.emplogin.param.null")] UI_EmpPwdLogin param )
        {
            return this._Service.EmpPwdLogin(param.LoginName, param.Password, this.Request.ToLoginState());
        }

        public void LoginOut ()
        {
            base.CancelAccredit();
        }
        public void CheckIsLogin ()
        {
        }
    }
}

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
        /// 获取登陆资料
        /// </summary>
        /// <returns></returns>
        public LoginDatum GetLoginDatum ()
        {
            return this._Service.GetLoginDatum(base.UserState);
        }
        /// <summary>
        /// 人员密码登陆
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>登陆结果</returns>
        [ApiPower(false)]
        public EmpLoginRes EmpPwdLogin ( [NullValidate("hr.emplogin.param.null")] UI_EmpPwdLogin param )
        {
            return this._Service.EmpPwdLogin(param.LoginName, param.Password);
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

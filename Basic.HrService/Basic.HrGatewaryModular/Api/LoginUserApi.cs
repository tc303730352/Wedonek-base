using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.LoginUser;
using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class LoginUserApi : ApiController
    {
        private readonly ILoginUserService _Service;
        public LoginUserApi ( ILoginUserService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="empId">人员ID</param>
        [ApiPower("all", "hr.user.delete")]
        public void DeleteAccount ( [NumValidate("hr.loginuser.empId.error", 1)] long empId )
        {
            this._Service.DeleteAccount(empId);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="empId"></param>
        [ApiPower("all", "hr.user.pwd.reset")]
        public void ResetPwd ( [NumValidate("hr.loginuser.empId.error", 1)] long empId )
        {
            this._Service.ResetPwd(empId);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="set"></param>
        public void UpdatePwd ( PwdSetArg set )
        {
            this._Service.UpdatePwd(base.UserState.ToEmpId(), set);
        }
        /// <summary>
        /// 开通账号
        /// </summary>
        /// <param name="set">人员ID</param>
        [ApiPower("all", "hr.emp.open.account")]
        public void OpenAccount ( LongParam<long[]> set )
        {
            this._Service.OpenAccount(set.Value, set.Id);
        }
        /// <summary>
        /// 查询登陆账号信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>登陆用户资料</returns>
        public PagingResult<LoginUserDatum> Query ( [NullValidate("hr.loginuser.param.null")] UI_QueryLoginUser param )
        {
            param.Query.DeptId = base.UserState.PowerDeptId(param.Query.DeptId);
            return this._Service.QueryLoginUser(param.Query, param);
        }

    }
}

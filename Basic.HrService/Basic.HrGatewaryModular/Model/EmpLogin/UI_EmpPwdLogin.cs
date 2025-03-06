using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.EmpLogin
{
    /// <summary>
    /// 人员密码登陆 UI参数实体
    /// </summary>
    internal class UI_EmpPwdLogin
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        [NullValidate("hr.login.name.null")]
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [NullValidate("hr.login.password.null")]
        public string Password { get; set; }

    }
}

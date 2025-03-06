using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Model.LoginUser
{
    /// <summary>
    /// 查询登陆账号信息 UI参数实体
    /// </summary>
    internal class UI_QueryLoginUser : BasicPage
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        [NullValidate("public.query.param.null")]
        public LoginUserQuery Query { get; set; }

    }
}

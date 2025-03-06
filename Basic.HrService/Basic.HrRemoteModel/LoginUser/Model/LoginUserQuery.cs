using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.LoginUser.Model
{
    public class LoginUserQuery
    {
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        public long? UnitId { get; set; }

        public long[] DeptId { get; set; }

        public string QueryKey { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public HrUserType[] UserType { get; set; }

        /// <summary>
        /// 拥有的角色
        /// </summary>
        public long[] RoleId { get; set; }
    }
}

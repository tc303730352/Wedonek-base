using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Role.Model
{
    public class RoleSet
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [NullValidate("hr.role.name.null")]
        [LenValidate("hr.role.name.len", 2, 50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [LenValidate("hr.role.remark.len", 0, 100)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public long[] ProwerId { get; set; }
    }
}

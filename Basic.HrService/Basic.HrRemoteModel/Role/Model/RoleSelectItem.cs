namespace Basic.HrRemoteModel.Role.Model
{
    public class RoleSelectItem
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

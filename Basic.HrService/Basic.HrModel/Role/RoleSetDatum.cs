namespace Basic.HrModel.Role
{
    public class RoleSetDatum
    {
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

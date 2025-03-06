namespace Basic.HrRemoteModel.Role.Model
{
    /// <summary>
    /// 角色资料
    /// </summary>
    public class RoleDatum
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
        /// 是否默认角色
        /// </summary>
        public bool IsDefRole { get; set; }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }


        public int EmpNum { get; set; }
    }
}

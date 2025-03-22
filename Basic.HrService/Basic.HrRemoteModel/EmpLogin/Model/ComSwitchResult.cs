namespace Basic.HrRemoteModel.EmpLogin.Model
{
    public class ComSwitchResult
    {
        /// <summary>
        /// 功能菜单权限
        /// </summary>
        public string[] Power { get; set; }

        /// <summary>
        /// 部门权限
        /// </summary>
        public long[] DeptId { get; set; }

        public bool IsAdmin { get; set; }
    }
}

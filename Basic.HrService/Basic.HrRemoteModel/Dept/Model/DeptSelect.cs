namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptSelect
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public long? LeaderId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为独立单位
        /// </summary>
        public bool IsUnit { get; set; }
    }
}

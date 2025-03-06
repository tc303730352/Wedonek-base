namespace Basic.HrRemoteModel.Unit.Model
{
    public class UnitTree
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
        /// 下级部门
        /// </summary>
        public UnitTree[] Children { get; set; }
    }
}

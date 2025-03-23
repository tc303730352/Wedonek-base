namespace Basic.HrRemoteModel.Dept.Model
{
    /// <summary>
    /// 部门树
    /// </summary>
    public class DeptTree
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

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
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 下级部门
        /// </summary>
        public DeptTree[] Children { get; set; }
    }
}

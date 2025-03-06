namespace Basic.HrRemoteModel.DeptChange.Model
{
    public class MergeEmp
    {
        /// <summary>
        /// 原单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 原单位名
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 合并后单位ID
        /// </summary>
        public long? ToUnitId { get; set; }

        /// <summary>
        /// 合并后单位名
        /// </summary>
        public string ToUnitName { get; set; }
        /// <summary>
        /// 人员列表
        /// </summary>
        public MergeEmpDto[] Emps { get; set; }
        public string DeptName { get; set; }
        public string ToDeptName { get; set; }
    }
}

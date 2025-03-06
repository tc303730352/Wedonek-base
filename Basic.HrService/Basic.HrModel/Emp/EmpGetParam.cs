using Basic.HrRemoteModel;

namespace Basic.HrModel.Emp
{
    public class EmpGetParam
    {
        public long CompanyId { get; set; }
        public long[] DeptId { get; set; }

        public HrEmpStatus[] Status { get; set; }

        /// <summary>
        /// 是否限定入职部门
        /// </summary>
        public bool IsEntry { get; set; }
    }
}

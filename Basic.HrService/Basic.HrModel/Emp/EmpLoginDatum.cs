using Basic.HrRemoteModel;

namespace Basic.HrModel.Emp
{
    public class EmpLoginDatum
    {
        public long EmpId { get; set; }

        public long CompanyId { get; set; }
        public long DeptId { get; set; }

        public long UnitId { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }

        public HrEmpStatus Status { get; set; }
        public bool IsOpenAccount { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}

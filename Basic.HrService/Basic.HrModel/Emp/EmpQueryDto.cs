using Basic.HrRemoteModel;

namespace Basic.HrModel.Emp
{
    public class EmpQueryDto
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 是否只返回入职的员工
        /// </summary>
        public bool IsEntry { get; set; }

        /// <summary>
        /// 查询Key
        /// </summary>
        public string QueryKey { get; set; }

        /// <summary>
        /// 所在部门
        /// </summary>
        public long[] DeptId { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public long? UnitId { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public HrEmpStatus[] Status { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public HrUserType? UserType { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string[] Post { get; set; }
        /// <summary>
        /// 职务ID
        /// </summary>
        public string[] Title { get; set; }
    }
}

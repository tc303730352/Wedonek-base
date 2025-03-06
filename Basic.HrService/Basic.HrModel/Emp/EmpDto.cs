using Basic.HrRemoteModel;

namespace Basic.HrModel.Emp
{
    public class EmpDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }


        /// <summary>
        /// 入职的部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HrUserSex Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string UserHead { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public HrUserType UserType { get; set; }


        /// <summary>
        /// 岗位编号
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HrEmpStatus Status { get; set; }

    }
}

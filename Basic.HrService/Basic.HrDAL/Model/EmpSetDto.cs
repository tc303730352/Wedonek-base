using Basic.HrRemoteModel;

namespace Basic.HrDAL.Model
{
    public class EmpSetDto
    {
        /// <summary>
        /// 入职的部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 所在单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HrUserSex Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string UserHead { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 备用联系电话
        /// </summary>
        public string BackupPhone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Show { get; set; }


        /// <summary>
        /// 岗位
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

    }
}

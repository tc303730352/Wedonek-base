using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Emp.Model
{
    public class EmpSet
    {
        /// <summary>
        /// 入职的部门ID
        /// </summary>
        [NumValidate("hr.dept.id.error", 1)]
        public long DeptId { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        [LenValidate("hr.emp.no.length", 0, 20)]
        [FormatValidate("hr.emp.no.error", ValidateFormat.数字字母)]
        public string EmpNo { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [NullValidate("hr.emp.name.null")]
        [LenValidate("hr.emp.name.length", 0, 50)]
        public string EmpName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [EnumValidate("hr.emp.sex.error", typeof(HrUserSex))]
        public HrUserSex Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [NullValidate("hr.emp.phone.null")]
        [LenValidate("hr.emp.phone.length", 11, 20)]
        [FormatValidate("hr.emp.phone.error", ValidateFormat.手机号)]
        public string Phone { get; set; }

        /// <summary>
        /// 备用联系电话
        /// </summary>
        [LenValidate("hr.emp.backup.phone.length", 11, 20)]
        [FormatValidate("hr.emp.backup.phone.error", ValidateFormat.手机号)]
        public string BackupPhone { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        [LenValidate("hr.emp.idcard.length", 18)]
        [FormatValidate("hr.emp.idcard.error", ValidateFormat.身份证号)]
        public string IDCard { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        [TimeValidate("hr.emp.birthday.error", 0, TimeFormat.日, true)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [LenValidate("hr.emp.email.length", 0, 100)]
        [FormatValidate("hr.emp.email.error", ValidateFormat.Email)]
        public string Email { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [LenValidate("hr.emp.show.length", 0, 255)]
        public string Show { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>

        [LenValidate("hr.post.code.len", 2, 20)]
        [FormatValidate("hr.post.code.error", ValidateFormat.数字字母)]
        public string PostCode { get; set; }

        /// <summary>
        /// 职位码
        /// </summary>
        [NullValidate("hr.title.code.null")]
        [LenValidate("hr.title.code.len", 1, 10)]
        [FormatValidate("hr.title.code.error", ValidateFormat.数字字母)]
        public string[] Title { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [LenValidate("hr.nation.code.len", 2, 20)]
        [FormatValidate("hr.nation.code.error", ValidateFormat.数字字母)]
        public string Nation { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [LenValidate("hr.emp.address.length", 0, 100)]
        public string Address { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [LenValidate("hr.emp.user.head.length", 0, 510)]
        [FormatValidate("hr.emp.user.head.error", ValidateFormat.图片URI)]
        public string UserHead { get; set; }
        /// <summary>
        /// 文件ID
        /// </summary>
        public long? FileId { get; set; }

    }
}

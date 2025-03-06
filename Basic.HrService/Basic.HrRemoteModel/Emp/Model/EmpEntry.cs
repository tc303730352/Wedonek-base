using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Emp.Model
{
    public class EmpEntry
    {
        /// <summary>
        /// 入职的部门ID
        /// </summary>
        [NumValidate("hr.dept.id.error", 1)]
        public long DeptId { get; set; }

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
        [LenValidate("hr.title.code.len", 2, 20)]
        [FormatValidate("hr.title.code.error", ValidateFormat.数字字母)]
        public string[] Title { get; set; }

        public long UnitId { get; set; }
    }
}

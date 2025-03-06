using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.LoginUser.Model
{
    public class PwdSetArg
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [NullValidate("hr.emp.pwd.null")]
        [LenValidate("hr.emp.pwd.len", 32)]
        [FormatValidate("hr.emp.pwd.error", ValidateFormat.数字字母)]
        [ComparisonValidate("hr.emp.new.old.pwd.equal", "NewPwd", false)]
        public string Pwd
        {
            get;
            set;
        }

        /// <summary>
        /// 新密码
        /// </summary>
        [NullValidate("hr.emp.new.pwd.null")]
        [LenValidate("hr.emp.new.pwd.len", 32)]
        [FormatValidate("hr.emp.new.pwd.error", ValidateFormat.数字字母)]
        public string NewPwd
        {
            get;
            set;
        }
    }
}

using Basic.HrRemoteModel;

namespace Basic.HrModel.LoginUser
{
    public class LoginUser
    {
        public long EmpId { get; set; }

        /// <summary>
        /// 登陆用户
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登陆类型
        /// </summary>
        public AccountType LoginType { get; set; }
    }
}

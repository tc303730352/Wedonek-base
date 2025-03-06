using Basic.HrRemoteModel;
using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("LoginUser")]
    public class DBLoginUser
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
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

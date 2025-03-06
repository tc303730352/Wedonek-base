using Basic.HrModel.DB;

namespace Basic.HrModel.LoginUser
{
    public class AddLoginUser
    {
        public LoginUserDatum[] Emps
        {
            get;
            set;
        }
        public DBEmpDeptPrower[] DeptPrower
        {
            get;
            set;
        }
        public List<DBLoginUser> Account { get; set; }
    }
}

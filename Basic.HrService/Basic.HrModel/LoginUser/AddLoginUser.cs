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
        public DBEmpDeptPower[] DeptPower
        {
            get;
            set;
        }
        public List<DBLoginUser> Account { get; set; }
    }
}

using Basic.HrModel.EmpTitle;

namespace Basic.HrModel.LoginUser
{
    public class OpenAccountParam
    {
        public LoginUserDatum[] Emps
        {
            get;
            set;
        }
        public long CompanyId
        {
            get;
            set;
        }
        public EmpTitleDto[] EmpTitle
        {
            get;
            set;
        }
    }
}

using Basic.HrModel.DB;
using Basic.HrModel.LoginUser;

namespace Basic.HrCollect
{
    public interface ILoginUserCollect
    {
        DBLoginUser[] GetAccounts (long[] empId);
        bool DeleteAccount (DBEmpList emp);
        long GetEmpId (string loginName);
        void OpenAccount (OpenAccountParam open);
        void SetAccount (DBEmpList emp);
    }
}
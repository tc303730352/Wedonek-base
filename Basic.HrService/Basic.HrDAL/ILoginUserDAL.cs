using Basic.HrModel.DB;
using Basic.HrModel.LoginUser;
using Basic.HrRemoteModel;

namespace Basic.HrDAL
{
    public interface ILoginUserDAL : IBasicDAL<DBLoginUser, long>
    {
        void Add (AddLoginUser add);
        void Clear (long empId);
        long GetEmpId (string loginName, AccountType accountType);
        void Set (long empId, long[] dropId, List<LoginUser> users);
    }
}
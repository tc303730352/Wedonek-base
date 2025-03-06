using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.LoginUser;
using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class LoginUserService : ILoginUserService
    {
        public void DeleteAccount (long empId)
        {
            new DeleteAccount
            {
                EmpId = empId,
            }.Send();
        }
        public void ResetPwd (long empId)
        {
            new ResetUserPwd
            {
                EmpId = empId
            }.Send();
        }
        public void UpdatePwd (long empId, PwdSetArg set)
        {
            new UpdateUserPwd
            {
                EmpId = empId,
                SetArg = set
            }.Send();
        }
        public void OpenAccount (long[] empId, long companyId)
        {
            new OpenAccount
            {
                EmpId = empId,
                CompanyId = companyId
            }.Send();
        }

        public LoginUserDatum[] QueryLoginUser (LoginUserQuery query, IBasicPage paging, out int count)
        {
            return new QueryLoginUser
            {
                Query = query,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out count);
        }

    }
}

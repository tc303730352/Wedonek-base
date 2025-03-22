using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.LoginUser;
using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class LoginUserService : ILoginUserService
    {
        public void DeleteAccount ( long empId )
        {
            new DeleteAccount
            {
                EmpId = empId,
            }.Send();
        }
        public void ResetPwd ( long empId )
        {
            new ResetUserPwd
            {
                EmpId = empId
            }.Send();
        }
        public void UpdatePwd ( long empId, PwdSetArg set )
        {
            new UpdateUserPwd
            {
                EmpId = empId,
                SetArg = set
            }.Send();
        }
        public void OpenAccount ( long[] empId, long companyId )
        {
            new OpenAccount
            {
                EmpId = empId,
                CompanyId = companyId
            }.Send();
        }

        public PagingResult<LoginUserDatum> QueryLoginUser ( LoginUserQuery query, IBasicPage paging )
        {
            if ( query.DeptId != null && query.DeptId.Length == 0 )
            {
                return new PagingResult<LoginUserDatum>();
            }
            LoginUserDatum[] list = new QueryLoginUser
            {
                Query = query,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out int count);
            return new PagingResult<LoginUserDatum>(list, count);
        }

    }
}

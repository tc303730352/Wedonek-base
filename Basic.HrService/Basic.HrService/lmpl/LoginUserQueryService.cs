using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel.Emp.Model;
using Basic.HrRemoteModel.EmpRole.Model;
using Basic.HrRemoteModel.LoginUser.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class LoginUserQueryService : ILoginUserQueryService
    {
        private readonly ILoginUserCollect _LoginUser;
        private readonly IEmpCollect _Emp;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IEmpDeptPowerCollect _DeptPower;
        public LoginUserQueryService ( ILoginUserCollect loginUser,
            IEmpCollect emp,
            IEmpRoleCollect role,
            IEmpDeptPowerCollect deptPower )
        {
            this._DeptPower = deptPower;
            this._EmpRole = role;
            this._LoginUser = loginUser;
            this._Emp = emp;
        }
        public PagingResult<LoginUserDatum> Query ( LoginUserQuery query, IBasicPage paging )
        {
            EmpBaseDto[] users = this._Emp.Query<EmpBaseDto>(new EmpQuery
            {
                IsNoOpen = false,
                QueryKey = query.QueryKey,
                CompanyId = query.CompanyId,
                DeptId = query.DeptId,
                UnitId = query.UnitId,
                IsEntry = false,
                UserType = query.UserType,
                RoleId = query.RoleId
            }, paging, out int count);
            if ( users.IsNull() )
            {
                return new PagingResult<LoginUserDatum>(count);
            }
            long[] empId = users.ConvertAll(c => c.EmpId);
            DBLoginUser[] account = this._LoginUser.GetAccounts(empId);
            LoginUserDatum[] list = users.ConvertMap<EmpBaseDto, LoginUserDatum>();
            Dictionary<long, EmpRole[]> roles = this._EmpRole.GetRoles(query.CompanyId, empId);
            Dictionary<long, int> powerNum = this._DeptPower.GetPowerNum(empId, query.CompanyId);
            list.ForEach(a =>
            {
                a.Phone = account.Find(c => c.EmpId == a.EmpId && c.LoginType == HrRemoteModel.AccountType.手机号, c => c.LoginName);
                a.Email = account.Find(c => c.EmpId == a.EmpId && c.LoginType == HrRemoteModel.AccountType.Email, c => c.LoginName);
                a.LoginName = account.Find(c => c.EmpId == a.EmpId && c.LoginType == HrRemoteModel.AccountType.登陆名, c => c.LoginName);
                EmpRole[] role = roles.GetValueOrDefault(a.EmpId);
                if ( role != null )
                {
                    a.IsAdmin = role.IsExists(c => c.IsAdmin);
                    a.Role = role.ConvertAll(a => a.RoleName);
                }
                if ( !a.IsAdmin )
                {
                    a.DeptNum = powerNum.GetValueOrDefault(a.EmpId);
                }
            });
            return new PagingResult<LoginUserDatum>(list, count);
        }
    }
}

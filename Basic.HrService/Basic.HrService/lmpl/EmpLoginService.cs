using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.EmpLogin.Model;
using Basic.HrRemoteModel.EmpRole.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class EmpLoginService : IEmpLoginService
    {
        private static readonly string[] _AllPower = new string[] { "all" };
        private readonly ILoginUserCollect _LoginUser;
        private readonly IEmpCollect _Emp;
        private readonly IHrConfig _Config;
        private readonly IRoleOperatePowerCollect _RolePower;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IEmpDeptPowerCollect _DeptPower;
        private readonly IUserLoginLogSaveCollect _LoginLog;
        public EmpLoginService ( ILoginUserCollect loginUser,
            IEmpCollect emp,
            IHrConfig config,
            IEmpTitleCollect empTitle,
            IRoleOperatePowerCollect rolePower,
            IEmpDeptPowerCollect deptPower,
            IUserLoginLogSaveCollect loginLog,
            IEmpRoleCollect empRole,
            IDeptCollect dept )
        {
            this._LoginLog = loginLog;
            this._DeptPower = deptPower;
            this._LoginUser = loginUser;
            this._Emp = emp;
            this._Config = config;
            this._RolePower = rolePower;
            this._EmpRole = empRole;
        }
        public ComSwitchResult Switch ( long empId, long companyId )
        {
            EmpRole[] roles = this._EmpRole.GetRoles(companyId, empId);
            if ( roles.IsNull() )
            {
                throw new ErrorException("hr.user.no.config.role");
            }
            bool isAdmin = roles.IsExists(a => a.IsAdmin);
            string[] power = _AllPower;
            long[] deptId = null;
            if ( !isAdmin )
            {
                power = this._RolePower.GetOperateVal(roles.ConvertAll(a => a.RoleId));
                deptId = this._DeptPower.GetDeptId(empId, companyId);
            }
            return new ComSwitchResult
            {
                Power = power,
                DeptId = deptId,
                IsAdmin = isAdmin,
            };
        }
        public LoginResult Login ( long empId )
        {
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            return this._EmpLogin(emp);
        }
        private LoginResult _EmpLogin ( DBEmpList emp )
        {
            if ( emp.Status != HrEmpStatus.启用 )
            {
                throw new ErrorException("hr.user.no.enable");
            }
            else if ( emp.DeptId == 0 )
            {
                throw new ErrorException("hr.user.no.set.dept");
            }
            EmpRoleBase[] roles = this._EmpRole.GetEmpRoles(emp.EmpId);
            if ( roles.IsNull() )
            {
                throw new ErrorException("hr.user.no.config.role");
            }
            bool isAdmin = roles.IsExists(a => a.IsAdmin && a.CompanyId == emp.CompanyId);
            string[] power = _AllPower;
            long[] deptId = null;
            if ( !isAdmin )
            {
                power = this._RolePower.GetOperateVal(roles.Convert(a => a.CompanyId == emp.CompanyId, a => a.RoleId).Distinct());
                deptId = this._DeptPower.GetDeptId(emp.EmpId, emp.CompanyId);
            }
            return new LoginResult
            {
                EmpId = emp.EmpId,
                CompanyId = emp.CompanyId,
                Power = power,
                DeptId = deptId,
                Company = roles.Distinct(a => a.CompanyId),
                IsAdmin = isAdmin,
            };
        }
        private LoginResult _Login ( string username, string password )
        {
            long empId = this._LoginUser.GetEmpId(username);
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            string pwd = this._Config.EncryptionPwd(password, empId);
            if ( emp.EmpPwd != pwd )
            {
                throw new ErrorException("hr.user.pwd.error");
            }
            return this._EmpLogin(emp);
        }
        public LoginResult PwdLogin ( string username, string password, LoginState state )
        {
            try
            {
                LoginResult result = this._Login(username, password);
                this._LoginLog.Add(username, state);
                return result;
            }
            catch ( ErrorException ex )
            {
                this._LoginLog.AddFailLog(username, ex, state);
                throw;
            }
        }
    }
}

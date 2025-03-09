using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.EmpLogin.Model;
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
        private readonly IEmpTitleCollect _EmpTitle;
        private readonly IRoleOperatePowerCollect _RolePower;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IRoleCollect _Role;
        private readonly IDeptCollect _Dept;
        public EmpLoginService ( ILoginUserCollect loginUser,
            IEmpCollect emp,
            IHrConfig config,
            IEmpTitleCollect empTitle,
            IRoleOperatePowerCollect rolePower,
            IEmpRoleCollect empRole,
            IRoleCollect role,
            IDeptCollect dept )
        {
            this._LoginUser = loginUser;
            this._Emp = emp;
            this._Config = config;
            this._EmpTitle = empTitle;
            this._RolePower = rolePower;
            this._EmpRole = empRole;
            this._Role = role;
            this._Dept = dept;
        }

        public LoginResult Login ( long empId )
        {
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            return this._Login(emp);
        }
        private LoginResult _Login ( DBEmpList emp )
        {
            if ( emp.Status != HrEmpStatus.启用 )
            {
                throw new ErrorException("hr.user.no.enable");
            }
            else if ( emp.DeptId == 0 )
            {
                throw new ErrorException("hr.user.no.set.dept");
            }
            long[] roleId = this._EmpRole.GetRoleId(emp.EmpId);
            if ( roleId.IsNull() )
            {
                throw new ErrorException("hr.user.no.config.role");
            }
            bool isAdmin = this._Role.CheckIsAdmin(roleId);
            if ( roleId.IsNull() && isAdmin == false )
            {
                throw new ErrorException("hr.user.no.power");
            }
            DBDept dept = this._Dept.Get(emp.DeptId);
            string[] title = this._EmpTitle.GetTitle(emp.EmpId, emp.DeptId);
            string[] power = isAdmin ? _AllPower : this._RolePower.GetOperateVal(roleId);
            return new LoginResult
            {
                EmpId = emp.EmpId,
                DeptId = emp.DeptId,
                CompanyId = emp.CompanyId,
                Post = emp.PostCode,
                Title = title,
                UnitId = dept.UnitId,
                Power = power,
                EmpName = emp.EmpName,
                IsAdmin = isAdmin
            };
        }
        public LoginResult PwdLogin ( string username, string password )
        {
            long empId = this._LoginUser.GetEmpId(username);
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            string pwd = this._Config.EncryptionPwd(password, empId);
            if ( emp.EmpPwd != pwd )
            {
                throw new ErrorException("hr.user.pwd.error");
            }
            return this._Login(emp);
        }
    }
}

using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrModel.LoginUser;
using Basic.HrRemoteModel;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class LoginUserService : ILoginUserService
    {
        private readonly ILoginUserCollect _LoginUser;
        private readonly IEmpCollect _Emp;
        private readonly IHrConfig _HrConfig;
        private readonly IRoleCollect _Role;
        private readonly IEmpTitleCollect _EmpTitle;
        public LoginUserService ( ILoginUserCollect loginUser,
            IEmpCollect emp,
            IEmpTitleCollect empTitle,
            IHrConfig config,
            IRoleCollect role )
        {
            this._EmpTitle = empTitle;
            this._HrConfig = config;
            this._Role = role;
            this._LoginUser = loginUser;
            this._Emp = emp;
        }

        public void Open ( long[] empId, long companyId )
        {
            EmpLoginDatum[] emps = this._Emp.Gets<EmpLoginDatum>(empId).FindAll(c => c.CompanyId == companyId && c.DeptId != 0 && c.Status == HrEmpStatus.启用 && c.IsOpenAccount == false);
            if ( emps.Length == 0 )
            {
                return;
            }
            long roleId = this._Role.GetDefRoleId(companyId);
            OpenAccountParam param = new OpenAccountParam
            {
                CompanyId = companyId,
                Emps = emps.ConvertAll(a => new LoginUserDatum
                {
                    Email = a.Email,
                    EmpId = a.EmpId,
                    EmpNo = a.EmpNo,
                    Phone = a.Phone,
                    Pwd = this._HrConfig.EncryptionFullPwd(this._HrConfig.InitPwd, a.EmpId),
                    RoleId = roleId
                }),
                EmpTitle = this._EmpTitle.GetEmpTitle(empId, companyId)
            };
            this._LoginUser.OpenAccount(param);
        }


        public void Delete ( long empId )
        {
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            if ( this._LoginUser.DeleteAccount(emp) )
            {
                new UserChangeEvent(empId).AsyncSend("Delete");
            }
        }


    }
}

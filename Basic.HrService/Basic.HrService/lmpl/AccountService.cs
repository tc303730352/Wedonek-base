using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrRemoteModel;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class AccountService : IAccountService
    {
        private readonly IHrConfig _Config;
        private readonly IEmpCollect _Emp;

        public AccountService (IHrConfig config, IEmpCollect emp)
        {
            this._Config = config;
            this._Emp = emp;
        }
        public void ResetPwd (long empId)
        {
            var emp = this._Emp.Get(empId, a => new
            {
                a.EmpPwd,
                a.Status
            });
            if (emp.Status != HrEmpStatus.启用)
            {
                throw new ErrorException("hr.emp.no.enable");
            }
            string newPwd = this._Config.EncryptionFullPwd(this._Config.InitPwd, empId);
            if (emp.EmpPwd == newPwd)
            {
                return;
            }
            this._Emp.SetEmpPwd(empId, newPwd);
            new UserChangeEvent(empId).AsyncSend("PwdChange");
        }
        public void UpdatePwd (long empId, string newPwd, string oldPwd)
        {
            var emp = this._Emp.Get(empId, a => new
            {
                a.EmpPwd,
                a.Status
            });
            if (emp.Status != HrEmpStatus.启用)
            {
                throw new ErrorException("hr.emp.no.enable");
            }
            oldPwd = this._Config.EncryptionPwd(oldPwd, empId);
            if (emp.EmpPwd != oldPwd)
            {
                throw new ErrorException("hr.emp.pwd.error");
            }
            newPwd = this._Config.EncryptionPwd(newPwd, empId);
            if (emp.EmpPwd == newPwd)
            {
                return;
            }
            this._Emp.SetEmpPwd(empId, newPwd);
            new UserChangeEvent(empId).AsyncSend("PwdChange");
        }
    }
}

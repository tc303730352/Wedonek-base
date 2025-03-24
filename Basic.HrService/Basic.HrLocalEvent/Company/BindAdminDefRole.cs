using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Company
{
    [LocalEventName("SetAdmin")]
    internal class BindAdminDefRole : IEventHandler<CompanyEvent>
    {
        private readonly IRoleCollect _Role;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IEmpCollect _Emp;
        private readonly IEmpTitleCollect _EmpTitle;

        public BindAdminDefRole ( IRoleCollect role,
            IEmpRoleCollect empRole,
            IEmpCollect emp,
            IEmpTitleCollect empTitle )
        {
            this._Role = role;
            this._EmpRole = empRole;
            this._Emp = emp;
            this._EmpTitle = empTitle;
        }

        public void HandleEvent ( CompanyEvent data, string eventName )
        {
            if ( data.OldAdminId.HasValue )
            {
                long adminId = data.OldAdminId.Value;
                long comId = this._Emp.Get(adminId, a => a.CompanyId);
                if ( comId == data.Company.Id || this._EmpTitle.CheckIsExists(adminId, data.Company.Id) )
                {
                    return;
                }
                this._EmpRole.Clear(data.Company.Id, adminId);
            }
            if ( data.Company.AdminId.HasValue )
            {
                long adminId = data.Company.AdminId.Value;
                long comId = this._Emp.Get(adminId, a => a.CompanyId);
                if ( comId == data.Company.Id || this._EmpTitle.CheckIsExists(adminId, data.Company.Id) )
                {
                    return;
                }
                long defRoleId = this._Role.GetDefRoleId(data.Company.Id);
                _ = this._EmpRole.SetRole(adminId, data.Company.Id, new long[] { defRoleId });
            }
        }
    }
}

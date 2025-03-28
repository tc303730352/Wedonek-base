using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.Modular;

namespace Basic.HrLocalEvent.Role
{
    [LocalEventName("SetIsEnable", "SetIsAdmin")]
    internal class RoleChangeKickOutUser : IEventHandler<RoleEvent>
    {
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IAccreditService _Accredit;
        public RoleChangeKickOutUser ( IEmpRoleCollect empRole, IAccreditService accredit )
        {
            this._Accredit = accredit;
            this._EmpRole = empRole;
        }

        public void HandleEvent ( RoleEvent data, string eventName )
        {
            if ( !data.Role.IsEnable )
            {
                return;
            }
            long[] empId = this._EmpRole.GetEmpId(data.Role.Id);
            if ( !empId.IsNull() )
            {
                empId.ForEach(c =>
                {
                    string key = string.Concat("Hr_", c);
                    this._Accredit.KickOutAccredit(key);
                });
            }
        }
    }
}

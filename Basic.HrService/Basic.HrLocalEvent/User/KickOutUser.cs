using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Modular;

namespace Basic.HrLocalEvent.User
{
    [LocalEventName("EmpDisable", "PwdChange", "RoleChange", "DeptPowerChange", "Delete", "Update")]
    internal class KickOutUser : IEventHandler<UserChangeEvent>
    {
        private readonly IAccreditService _Accredit;

        public KickOutUser ( IAccreditService accredit )
        {
            this._Accredit = accredit;
        }

        public void HandleEvent ( UserChangeEvent data, string eventName )
        {
            string key = string.Concat("Hr_", data.EmpId);
            this._Accredit.KickOutAccredit(key);
        }
    }
}

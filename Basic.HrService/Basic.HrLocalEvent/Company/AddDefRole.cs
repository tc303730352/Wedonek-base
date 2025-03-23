using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Company
{
    [LocalEventName("Add")]
    internal class AddDefRole : IEventHandler<CompanyEvent>
    {
        private readonly IRoleCollect _Role;

        public AddDefRole ( IRoleCollect role )
        {
            this._Role = role;
        }

        public void HandleEvent ( CompanyEvent data, string eventName )
        {
            this._Role.AddDefRole(data.Company.Id);
        }
    }
}

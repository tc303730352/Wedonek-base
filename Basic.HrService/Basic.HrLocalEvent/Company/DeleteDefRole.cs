using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Company
{
    [LocalEventName("Delete")]
    internal class DeleteDefRole : IEventHandler<CompanyEvent>
    {
        private readonly IRoleCollect _Role;

        public DeleteDefRole ( IRoleCollect role )
        {
            this._Role = role;
        }

        public void HandleEvent ( CompanyEvent data, string eventName )
        {
            this._Role.Clear(data.Company.Id);
        }
    }
}

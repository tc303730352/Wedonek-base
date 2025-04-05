using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.FormLocalEvent.Form
{
    [LocalEventName("Delete")]
    internal class ClearTable : IEventHandler<FormEvent>
    {
        private readonly ICustomTableCollect _Table;

        public ClearTable ( ICustomTableCollect table )
        {
            this._Table = table;
        }

        public void HandleEvent ( FormEvent data, string eventName )
        {
            this._Table.Clear(data.Form.Id);
        }
    }
}

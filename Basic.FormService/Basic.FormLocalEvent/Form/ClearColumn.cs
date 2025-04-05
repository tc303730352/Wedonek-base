using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.FormLocalEvent.Form
{
    [LocalEventName("Delete")]
    internal class ClearColumn : IEventHandler<FormEvent>
    {
        private readonly ITableColumnCollect _Column;

        public ClearColumn ( ITableColumnCollect column )
        {
            this._Column = column;
        }

        public void HandleEvent ( FormEvent data, string eventName )
        {
            this._Column.Clear(data.Form.Id);
        }
    }
}

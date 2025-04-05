using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.FormLocalEvent.Table
{
    [LocalEventName("Delete")]
    internal class ClearColumn : IEventHandler<TableEvent>
    {
        private readonly ITableColumnCollect _Column;

        public ClearColumn ( ITableColumnCollect column )
        {
            this._Column = column;
        }

        public void HandleEvent ( TableEvent data, string eventName )
        {
            this._Column.ClearByTableId(data.Table.Id);
        }
    }
}

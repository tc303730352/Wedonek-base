using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.FormLocalEvent.Group
{
    [LocalEventName("Delete")]
    internal class ClearColumn : IEventHandler<TableGroupEvent>
    {
        private readonly ITableColumnCollect _Column;

        public ClearColumn ( ITableColumnCollect column )
        {
            this._Column = column;
        }

        public void HandleEvent ( TableGroupEvent data, string eventName )
        {
            this._Column.ClearByGroupId(data.Group.Id);
        }
    }
}

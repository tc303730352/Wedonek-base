using Basic.FormModel.DB;
using WeDonekRpc.Client;

namespace Basic.FormLocalEvent.Model
{
    public class TableEvent : RpcLocalEvent
    {
        public TableEvent ( DBCustomTable table )
        {
            this.Table = table;
        }
        public DBCustomTable Table { get; }
    }
}

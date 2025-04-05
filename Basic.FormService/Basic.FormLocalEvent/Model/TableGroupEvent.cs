using Basic.FormModel.DB;
using WeDonekRpc.Client;

namespace Basic.FormLocalEvent.Model
{
    public class TableGroupEvent : RpcLocalEvent
    {
        public TableGroupEvent ( DBTableGroup group )
        {
            this.Group = group;
        }
        public DBTableGroup Group { get; }
    }
}

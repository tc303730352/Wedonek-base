using Basic.HrModel.DB;
using WeDonekRpc.Client;

namespace Basic.HrLocalEvent.Model
{
    public class DicDeleteEvent : RpcLocalEvent
    {
        public DicDeleteEvent (DBDicList dic)
        {
            this.Dic = dic;
        }
        public DBDicList Dic { get; set; }
    }
}

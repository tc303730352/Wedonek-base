using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.FormLocalEvent.Form
{
    [LocalEventName("Delete")]
    internal class ClearTableGroup : IEventHandler<FormEvent>
    {
        private readonly ITableGroupCollect _Group;

        public ClearTableGroup ( ITableGroupCollect group )
        {
            this._Group = group;
        }

        public void HandleEvent ( FormEvent data, string eventName )
        {
            this._Group.Clear(data.Form.Id);
        }
    }
}

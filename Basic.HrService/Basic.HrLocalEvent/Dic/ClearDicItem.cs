using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Dic
{
    [LocalEventName("Delete")]
    internal class ClearDicItem : IEventHandler<DicDeleteEvent>
    {
        private readonly IDicItemCollect _Service;

        public ClearDicItem (IDicItemCollect service)
        {
            this._Service = service;
        }

        public void HandleEvent (DicDeleteEvent data, string eventName)
        {
            if (data.Dic.IsTreeDic)
            {
                return;
            }
            this._Service.Clear(data.Dic.Id);
        }
    }
}

using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Dic
{
    [LocalEventName("Delete")]
    internal class ClearDicTreeItem : IEventHandler<DicDeleteEvent>
    {
        private readonly ITreeDicItemCollect _Service;

        public ClearDicTreeItem (ITreeDicItemCollect service)
        {
            this._Service = service;
        }

        public void HandleEvent (DicDeleteEvent data, string eventName)
        {
            if (data.Dic.IsTreeDic)
            {
                this._Service.Clear(data.Dic.Id);
            }
        }
    }
}

using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using Basic.FormRemoteModel;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.FormLocalEvent.Table
{
    [LocalEventName("Delete")]
    internal class ClearTableGroup : IEventHandler<TableEvent>
    {
        private readonly ITableGroupCollect _Group;

        public ClearTableGroup ( ITableGroupCollect group )
        {
            this._Group = group;
        }

        public void HandleEvent ( TableEvent data, string eventName )
        {
            if ( data.Table.TableType == FormTableType.多行列表 )
            {
                this._Group.ClearByTableId(data.Table.Id);
            }
        }
    }
}

using Basic.FormRemoteModel.Table;
using Basic.FormRemoteModel.Table.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.FormService.RpcEvent
{
    internal class TableEvent : IRpcApiService
    {
        private readonly ICustomTableService _Table;

        public TableEvent ( ICustomTableService table )
        {
            this._Table = table;
        }

        public long AddTable ( AddTable obj )
        {
            return this._Table.Add(obj.Datum);
        }
        public bool SetTable ( SetTable obj )
        {
            return this._Table.Set(obj.Id, obj.Datum);
        }
        public CustomTable GetTable ( GetTable obj )
        {
            return this._Table.Get(obj.Id);
        }
        public void SetTableSort ( SetTableSort obj )
        {
            this._Table.SetSort(obj.Sort);
        }
        public void DeleteTable ( DeleteTable obj )
        {
            this._Table.Delete(obj.Id);
        }
    }
}

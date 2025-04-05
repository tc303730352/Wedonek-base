using Basic.FormRemoteModel.Column;
using Basic.FormRemoteModel.Column.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.FormService.RpcEvent
{
    internal class TableColumnEvent : IRpcApiService
    {
        private readonly ITableColumnService _Service;

        public TableColumnEvent ( ITableColumnService service )
        {
            this._Service = service;
        }
        public void SaveColumnSpan ( SaveColumnSpan obj )
        {
            this._Service.SetColSpan(obj.ColSpan);
        }
        public long AddColumn ( AddColumn obj )
        {
            return this._Service.Add(obj.Datum);
        }
        public bool SetColumn ( SetColumn obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
        public TableColumnDto GetColumn ( GetColumn obj )
        {
            return this._Service.Get(obj.Id);
        }
        public void SetColumnSort ( SetColumnSort obj )
        {
            this._Service.SetSort(obj.Sort);
        }
        public bool SetColumnGroupId ( SetColumnGroupId obj )
        {
            return this._Service.SetGroupId(obj.Id, obj.GroupId);
        }
        public bool SetColumnSpan ( SetColumnSpan obj )
        {
            return this._Service.SetColSpan(obj.Id, obj.ColSpan);
        }
        public void DeleteColumn ( DeleteColumn obj )
        {
            this._Service.Delete(obj.Id);
        }
    }
}
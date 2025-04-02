using Basic.FormRemoteModel.TableGroup;
using Basic.FormRemoteModel.TableGroup.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.FormService.RpcEvent
{
    internal class TableGroupEvent : IRpcApiService
    {
        private readonly ITableGroupService _Service;

        public TableGroupEvent ( ITableGroupService service )
        {
            this._Service = service;
        }

        public long AddTableGroup ( AddTableGroup obj )
        {
            return this._Service.Add(obj.Datum);
        }
        public bool SetTableGroup ( SetTableGroup obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
        public TableGroupDto GetTableGroup ( GetTableGroup obj )
        {
            return this._Service.Get(obj.Id);
        }
        public void SetTableGroupSort ( SetTableGroupSort obj )
        {
            this._Service.SetSort(obj.Sort);
        }
        public void DeleteTableGroup ( DeleteTableGroup obj )
        {
            this._Service.Delete(obj.Id);
        }
    }
}
using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Table.Model;
using Basic.FormService.Interface;

namespace Basic.FormService.lmpl
{
    internal class CustomTableService : ICustomTableService
    {
        private readonly ICustomTableCollect _Table;

        public CustomTableService ( ICustomTableCollect table )
        {
            this._Table = table;
        }

        public long Add ( CustomTableAdd data )
        {
            return this._Table.Add(data);
        }


        public void Delete ( long id )
        {
            DBCustomTable table = this._Table.Get(id);
            this._Table.Delete(table);
            new TableEvent(table).AsyncSend("Delete");
        }

        public CustomTable Get ( long id )
        {
            return this._Table.Get<CustomTable>(id);
        }


        public bool Set ( long id, CustomTableSet set )
        {
            DBCustomTable table = this._Table.Get(id);
            return this._Table.Set(table, set);
        }

        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._Table.SetSort(sort);
        }
    }
}

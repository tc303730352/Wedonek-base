using System.Linq.Expressions;
using Basic.FormDAL;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Table.Model;
using WeDonekRpc.Helper;

namespace Basic.FormCollect.lmpl
{
    internal class CustomTableCollect : ICustomTableCollect
    {
        private readonly ICustomTableDAL _Table;

        public CustomTableCollect ( ICustomTableDAL table )
        {
            this._Table = table;
        }
        public bool CheckIsNull ( long formId )
        {
            return this._Table.IsExists(c => c.FormId == formId) == false;
        }
        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._Table.SetSort(sort);
        }
        public bool Clear ( long formId )
        {
            long[] ids = this._Table.Gets(a => a.FormId == formId, a => a.Id);
            if ( ids.IsNull() )
            {
                return false;
            }
            this._Table.Delete(ids);
            return true;
        }
        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._Table.Gets<Result>(ids);
        }
        public Result[] GetsByFormId<Result> ( long formId ) where Result : class, new()
        {
            return this._Table.Gets<Result>(a => a.FormId == formId);
        }
        public Result[] Gets<Result> ( long[] ids, Expression<Func<DBCustomTable, Result>> selector )
        {
            return this._Table.Gets<Result>(ids, selector);
        }
        public long Add ( CustomTableAdd data )
        {
            if ( this._Table.IsExists(a => a.FormId == data.FormId && a.Title == data.Title) )
            {
                throw new ErrorException("form.table.title.repeat");
            }
            return this._Table.Add(data);
        }

        public DBCustomTable Get ( long id )
        {
            return this._Table.Get(id);
        }
        public Result Get<Result> ( long id ) where Result : class
        {
            return this._Table.Get<Result>(id);
        }
        public bool Set ( DBCustomTable source, CustomTableSet set )
        {
            if ( set.Title != source.Title && this._Table.IsExists(a => a.FormId == source.FormId && a.Title == set.Title) )
            {
                throw new ErrorException("form.table.title.repeat");
            }
            return this._Table.Update(source, set);
        }
        public void Delete ( DBCustomTable source )
        {
            this._Table.Delete(source.Id);
        }
    }
}

using System.Linq.Expressions;
using Basic.FormDAL;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.Helper;

namespace Basic.FormCollect.lmpl
{
    internal class TableGroupCollect : ITableGroupCollect
    {
        private readonly ITableGroupDAL _TableGroup;

        public TableGroupCollect ( ITableGroupDAL tableGroup )
        {
            this._TableGroup = tableGroup;
        }

        public bool CheckIsNull ( long formId )
        {
            return this._TableGroup.IsExists(c => c.FormId == formId) == false;
        }
        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._TableGroup.SetSort(sort);
        }
        public void ClearByTableId ( long tableId )
        {
            long[] ids = this._TableGroup.Gets(a => a.TableId == tableId, a => a.Id);
            if ( !ids.IsNull() )
            {
                this._TableGroup.Delete(ids);
            }
        }
        public void Clear ( long formId )
        {
            long[] ids = this._TableGroup.Gets(a => a.FormId == formId, a => a.Id);
            if ( !ids.IsNull() )
            {
                this._TableGroup.Delete(ids);
            }
        }
        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._TableGroup.Gets<Result>(ids);
        }
        public Result[] GetsByFormId<Result> ( long formId ) where Result : class, new()
        {
            return this._TableGroup.Gets<Result>(a => a.FormId == formId);
        }
        public Result[] Gets<Result> ( long[] ids, Expression<Func<DBTableGroup, Result>> selector )
        {
            return this._TableGroup.Gets<Result>(ids, selector);
        }
        public long Add ( TableGroupAdd data )
        {
            if ( this._TableGroup.IsExists(a => a.FormId == data.FormId && a.GroupName == data.GroupName) )
            {
                throw new ErrorException("form.table.group.name.repeat");
            }
            return this._TableGroup.Add(data);
        }
        public Result Get<Result> ( long id ) where Result : class
        {
            return this._TableGroup.Get<Result>(id);
        }
        public DBTableGroup Get ( long id )
        {
            return this._TableGroup.Get(id);
        }
        public bool Set ( DBTableGroup source, TableGroupSet set )
        {
            if ( set.GroupName != source.GroupName && this._TableGroup.IsExists(a => a.FormId == source.FormId && a.GroupName == set.GroupName) )
            {
                throw new ErrorException("form.table.group.name.repeat");
            }
            return this._TableGroup.Update(source, set);
        }
        public void Delete ( DBTableGroup source )
        {
            this._TableGroup.Delete(source.Id);
        }
    }
}

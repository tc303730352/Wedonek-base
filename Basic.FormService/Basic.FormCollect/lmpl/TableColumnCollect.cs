using System.Linq.Expressions;
using Basic.FormDAL;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.Helper;

namespace Basic.FormCollect.lmpl
{
    internal class TableColumnCollect : ITableColumnCollect
    {
        private readonly ITableColumnDAL _Column;

        public TableColumnCollect ( ITableColumnDAL column )
        {
            this._Column = column;
        }

        public bool CheckIsNull ( long formId )
        {
            return this._Column.IsExists(c => c.FormId == formId) == false;
        }
        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._Column.SetSort(sort);
        }
        public bool SetColSpan ( DBTableColumn source, int span )
        {
            if ( source.ColSpan == span )
            {
                return false;
            }
            this._Column.SetColSpan(source.Id, span);
            return true;
        }
        public bool SetGroupId ( DBTableColumn source, long groupId )
        {
            if ( source.GroupId == groupId )
            {
                return false;
            }
            this._Column.SetGroupId(source.Id, groupId);
            return true;
        }
        public void Clear ( long formId )
        {
            long[] ids = this._Column.Gets(a => a.FormId == formId, a => a.Id);
            if ( !ids.IsNull() )
            {
                this._Column.Delete(ids);
            }
        }
        public void ClearByTableId ( long tableId )
        {
            long[] ids = this._Column.Gets(a => a.TableId == tableId, a => a.Id);
            if ( !ids.IsNull() )
            {
                this._Column.Delete(ids);
            }
        }
        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._Column.Gets<Result>(ids);
        }
        public Result[] GetsByFormId<Result> ( long formId ) where Result : class, new()
        {
            return this._Column.Gets<Result>(a => a.FormId == formId);
        }
        public Result[] Gets<Result> ( long[] ids, Expression<Func<DBTableColumn, Result>> selector )
        {
            return this._Column.Gets<Result>(ids, selector);
        }
        public long Add ( TableColumnAdd data )
        {
            if ( this._Column.IsExists(a => a.TableId == data.TableId && a.ColName == data.ColName) )
            {
                throw new ErrorException("form.table.column.name.repeat");
            }
            return this._Column.Add(data);
        }

        public DBTableColumn Get ( long id )
        {
            return this._Column.Get(id);
        }
        public bool Set ( DBTableColumn source, TableColumnSet set )
        {
            if ( set.ColName != source.ColName && this._Column.IsExists(a => a.TableId == source.TableId && a.ColName == set.ColName) )
            {
                throw new ErrorException("form.table.column.name.repeat");
            }
            return this._Column.Update(source, set);
        }
        public void Delete ( DBTableColumn source )
        {
            this._Column.Delete(source.Id);
        }

        public Result Get<Result> ( long id ) where Result : class
        {
            return this._Column.Get<Result>(id);
        }

        public void ClearByGroupId ( long groupId )
        {
            long[] ids = this._Column.Gets(a => a.GroupId == groupId, a => a.Id);
            if ( !ids.IsNull() )
            {
                this._Column.Delete(ids);
            }
        }
    }
}

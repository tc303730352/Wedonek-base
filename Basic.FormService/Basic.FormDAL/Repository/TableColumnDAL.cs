using Basic.FormModel.DB;
using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class TableColumnDAL : BasicDAL<DBTableColumn, long>, ITableColumnDAL
    {
        public TableColumnDAL ( IRepository<DBTableColumn> basicDAL ) : base(basicDAL)
        {
        }
        public long Add ( TableColumnAdd data )
        {
            DBTableColumn add = data.ConvertMap<TableColumnAdd, DBTableColumn>();
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        public void SetColSpan ( long id, int span )
        {
            if ( !this._BasicDAL.Update(a => a.ColSpan == span, a => a.Id == id) )
            {
                throw new ErrorException("form.table.column.set.fail");
            }
        }
        public void SetGroupId ( long id, long groupId )
        {
            if ( !this._BasicDAL.Update(a => a.GroupId == groupId, a => a.Id == id) )
            {
                throw new ErrorException("form.table.column.set.fail");
            }
        }
        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            ISqlQueue<DBTableColumn> queue = this._BasicDAL.BeginQueue();
            sort.ForEach(c =>
            {
                queue.UpdateOneColumn(a => a.Sort == c.Value, a => a.Id == c.Key);
            });
            _ = queue.Submit();
        }
    }
}

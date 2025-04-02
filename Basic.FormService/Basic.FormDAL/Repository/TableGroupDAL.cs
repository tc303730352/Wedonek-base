using Basic.FormModel.DB;
using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class TableGroupDAL : BasicDAL<DBTableGroup, long>, ITableGroupDAL
    {
        public TableGroupDAL ( IRepository<DBTableGroup> basicDAL ) : base(basicDAL)
        {
        }
        public long Add ( TableGroupAdd data )
        {
            DBTableGroup add = data.ConvertMap<TableGroupAdd, DBTableGroup>();
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            ISqlQueue<DBTableGroup> queue = this._BasicDAL.BeginQueue();
            sort.ForEach(c =>
            {
                queue.UpdateOneColumn(a => a.Sort == c.Value, a => a.Id == c.Key);
            });
            _ = queue.Submit();
        }
    }
}

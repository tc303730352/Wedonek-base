using Basic.FormModel.DB;
using Basic.FormRemoteModel.Table.Model;
using LinqKit;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class CustomTableDAL : BasicDAL<DBCustomTable, long>, ICustomTableDAL
    {
        public CustomTableDAL(IRepository<DBCustomTable> basicDAL) : base(basicDAL)
        {
        }
        public long Add(CustomTableAdd data)
        {
            DBCustomTable add = data.ConvertMap<CustomTableAdd, DBCustomTable>();
            add.Id = IdentityHelper.CreateId();
            base._BasicDAL.Insert(add);
            return add.Id;
        }
        public void SetSort(KeyValuePair<long, int>[] sort)
        {
            ISqlQueue<DBCustomTable> queue = _BasicDAL.BeginQueue();
            sort.ForEach(c =>
            {
                queue.UpdateOneColumn(a => a.Sort == c.Value, a => a.Id == c.Key);
            });
            queue.Submit();
        }
    }
}

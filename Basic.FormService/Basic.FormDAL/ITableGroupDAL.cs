using Basic.FormModel.DB;
using Basic.FormRemoteModel.TableGroup.Model;

namespace Basic.FormDAL
{
    public interface ITableGroupDAL : IBasicDAL<DBTableGroup, long>
    {
        long Add ( TableGroupAdd data );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Table.Model;

namespace Basic.FormDAL
{
    public interface ICustomTableDAL : IBasicDAL<DBCustomTable, long>
    {
        void SetSort(KeyValuePair<long, int>[] sort);
        long Add(CustomTableAdd data);
    }
}
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Column.Model;

namespace Basic.FormDAL
{
    public interface ITableColumnDAL : IBasicDAL<DBTableColumn, long>
    {
        long Add ( TableColumnAdd data );
        void SetColSpan ( long id, int span );
        void SetColSpan ( KeyValuePair<long, int>[] span );
        void SetGroupId ( long id, long groupId );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
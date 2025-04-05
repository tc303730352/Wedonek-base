using Basic.FormRemoteModel.Column.Model;

namespace Basic.FormService.Interface
{
    public interface ITableColumnService
    {
        long Add ( TableColumnAdd data );
        void Delete ( long id );
        TableColumnDto Get ( long id );
        bool Set ( long id, TableColumnSet set );
        bool SetColSpan ( long id, int span );
        void SetColSpan ( KeyValuePair<long, int>[] span );
        bool SetGroupId ( long id, long groupId );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
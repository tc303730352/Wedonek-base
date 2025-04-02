using Basic.FormRemoteModel.Column.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface IColumnService
    {
        long Add ( TableColumnAdd datum );
        void Delete ( long id );
        TableColumnDto Get ( long id );
        bool Set ( long id, TableColumnSet datum );
        bool SetGroupId ( long id, long groupId );
        void SetSort ( KeyValuePair<long, int>[] sort );
        bool SetSpan ( long id, int colSpan );
    }
}
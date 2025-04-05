using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface IColumnService
    {
        long Add ( TableColumnAdd datum );
        void Delete ( long id );
        TableColumnDto Get ( long id );
        void SaveSpan ( LongNullParam<int>[] span );
        bool Set ( long id, TableColumnSet datum );
        bool SetGroupId ( long id, long groupId );
        void SetSort ( LongNullParam<int>[] sort );
        bool SetSpan ( long id, int colSpan );
    }
}
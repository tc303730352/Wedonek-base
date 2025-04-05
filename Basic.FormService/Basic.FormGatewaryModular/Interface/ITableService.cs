using Basic.FormRemoteModel.Table.Model;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface ITableService
    {
        long Add ( CustomTableAdd datum );
        void Delete ( long id );
        CustomTable Get ( long id );
        bool Set ( long id, CustomTableSet datum );
        void SetSort ( LongNullParam<int>[] sort );
    }
}
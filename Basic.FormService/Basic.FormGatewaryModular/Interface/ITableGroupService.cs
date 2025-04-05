using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface ITableGroupService
    {
        long Add ( TableGroupAdd datum );
        void Delete ( long id );
        TableGroupDto Get ( long id );
        bool Set ( long id, TableGroupSet datum );
        void SetSort ( LongNullParam<int>[] sort );
    }
}
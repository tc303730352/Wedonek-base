using Basic.FormRemoteModel.TableGroup.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface ITableGroupService
    {
        long Add ( TableGroupAdd datum );
        void Delete ( long id );
        TableGroupDto Get ( long id );
        bool Set ( long id, TableGroupSet datum );
        void SetSort ( Dictionary<long, int> sort );
    }
}
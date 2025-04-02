using Basic.FormRemoteModel.TableGroup.Model;

namespace Basic.FormService.Interface
{
    public interface ITableGroupService
    {
        long Add ( TableGroupAdd data );
        void Delete ( long id );
        TableGroupDto Get ( long id );
        bool Set ( long id, TableGroupSet set );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
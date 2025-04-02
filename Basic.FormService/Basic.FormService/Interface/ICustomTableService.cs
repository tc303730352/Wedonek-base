using Basic.FormRemoteModel.Table.Model;

namespace Basic.FormService.Interface
{
    public interface ICustomTableService
    {
        long Add ( CustomTableAdd data );
        void Delete ( long id );
        CustomTable Get ( long id );
        bool Set ( long id, CustomTableSet set );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
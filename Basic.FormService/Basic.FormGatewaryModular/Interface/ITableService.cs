using Basic.FormGatewaryModular.Model;
using Basic.FormRemoteModel.Table.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface ITableService
    {
        long Add ( CustomTableAdd datum );
        void Delete ( long id );
        CustomTable Get ( long id );
        bool Set ( long id, CustomTableSet datum );
        void SetSort ( SetSort[] sort );
    }
}
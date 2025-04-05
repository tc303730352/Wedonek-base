using System.Linq.Expressions;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Table.Model;

namespace Basic.FormCollect
{
    public interface ICustomTableCollect
    {
        long Add ( CustomTableAdd data );
        bool CheckIsNull ( long formId );
        void Clear ( long formId );
        void Delete ( DBCustomTable source );
        DBCustomTable Get ( long id );
        Result Get<Result> ( long id ) where Result : class;

        Result[] Gets<Result> ( long[] ids ) where Result : class;
        Result[] Gets<Result> ( long[] ids, Expression<Func<DBCustomTable, Result>> selector );
        Result[] GetsByFormId<Result> ( long formId ) where Result : class, new();
        bool Set ( DBCustomTable source, CustomTableSet set );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
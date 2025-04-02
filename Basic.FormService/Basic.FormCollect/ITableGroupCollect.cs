using System.Linq.Expressions;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.TableGroup.Model;

namespace Basic.FormCollect
{
    public interface ITableGroupCollect
    {
        void ClearByTableId ( long tableId );
        long Add ( TableGroupAdd data );
        bool CheckIsNull ( long formId );
        void Clear ( long formId );
        void Delete ( DBTableGroup source );
        DBTableGroup Get ( long id );
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        Result[] Gets<Result> ( long[] ids, Expression<Func<DBTableGroup, Result>> selector );
        Result[] GetsByFormId<Result> ( long formId ) where Result : class, new();
        bool Set ( DBTableGroup source, TableGroupSet set );
        void SetSort ( KeyValuePair<long, int>[] sort );
        Result Get<Result> ( long id ) where Result : class;
    }
}
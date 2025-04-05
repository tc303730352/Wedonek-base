using System.Linq.Expressions;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Column.Model;

namespace Basic.FormCollect
{
    public interface ITableColumnCollect
    {
        long Add ( TableColumnAdd data );
        bool CheckIsNull ( long formId );
        void Clear ( long formId );
        void ClearByTableId ( long tableId );

        void ClearByGroupId ( long groupId );

        void Delete ( DBTableColumn source );
        DBTableColumn Get ( long id );
        Result Get<Result> ( long id ) where Result : class;
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        Result[] Gets<Result> ( long[] ids, Expression<Func<DBTableColumn, Result>> selector );
        Result[] GetsByFormId<Result> ( long formId ) where Result : class, new();
        bool Set ( DBTableColumn source, TableColumnSet set );
        bool SetColSpan ( DBTableColumn source, int span );
        bool SetGroupId ( DBTableColumn source, long groupId );
        void SetSort ( KeyValuePair<long, int>[] sort );
    }
}
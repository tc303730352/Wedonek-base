using Basic.FormModel.DB;
using Basic.FormRemoteModel.Table.Model;
using System.Linq.Expressions;

namespace Basic.FormCollect
{
    public interface ICustomTableCollect
    {
        long Add(CustomTableAdd data);
        bool CheckIsNull(long formId);
        bool Clear(long formId);
        void Delete(DBCustomTable source);
        DBCustomTable Get(long id);
        Result[] Gets<Result>(long[] ids) where Result : class;
        Result[] Gets<Result>(long[] ids, Expression<Func<DBCustomTable, Result>> selector);
        Result[] GetsByFormId<Result>(long formId) where Result : class, new();
        bool Set(DBCustomTable source, CustomTableSet set);
    }
}
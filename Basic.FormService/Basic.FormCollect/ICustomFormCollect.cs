using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using System.Linq.Expressions;
using WeDonekRpc.Model;

namespace Basic.FormCollect
{
    public interface ICustomFormCollect
    {
        long Add(FormAdd data);
        void Delete(DBCustomForm source);
        DBCustomForm Get(long id);
        Result[] Gets<Result>(long[] ids) where Result : class;
        Result[] Gets<Result>(long[] ids, Expression<Func<DBCustomForm, Result>> selector);
        Result[] Query<Result>(FormQuery query, IBasicPage paging, out int count) where Result : class;
        bool Set(DBCustomForm source, FormSet set);
        bool SetStatus(DBCustomForm source, FormStatus status);
    }
}
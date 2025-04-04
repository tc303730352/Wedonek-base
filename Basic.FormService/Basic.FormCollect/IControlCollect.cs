using System.Linq.Expressions;
using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Model;

namespace Basic.FormCollect
{
    public interface IControlCollect
    {
        long Add ( ControlAdd data );
        void Delete ( DBCustomControl control );
        DBCustomControl Get ( long id );
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        Result[] GetEnables<Result> () where Result : class, new();

        Result[] Gets<Result> ( long[] ids, Expression<Func<DBCustomControl, Result>> selector );
        Result[] Query<Result> ( ControlQuery query, IBasicPage paging, out int count ) where Result : class;
        bool Set ( DBCustomControl control, ControlSet set );
        bool SetStatus ( DBCustomControl db, ControlStatus status );
    }
}
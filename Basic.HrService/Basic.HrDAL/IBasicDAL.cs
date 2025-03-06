using System.Linq.Expressions;

namespace Basic.HrDAL
{
    public interface IBasicDAL<T, IdentityId> where T : class, new()
    {
        bool IsExists (Expression<Func<T, bool>> where);
        int Count (Expression<Func<T, bool>> where);
        void Delete (IdentityId id);
        void Delete (IdentityId[] ids);
        T Get (IdentityId id);
        Result Get<Result> (IdentityId id) where Result : class;
        Result Get<Result> (IdentityId id, Expression<Func<T, Result>> selector);
        Result Get<Result> (Expression<Func<T, bool>> where, Expression<Func<T, Result>> selector);
        Result[] Gets<Result> (Expression<Func<T, bool>> where, Expression<Func<T, Result>> selector);

        Result[] Gets<Result> (Expression<Func<T, bool>> where) where Result : class, new();
        Result Get<Result> (Expression<Func<T, bool>> where) where Result : class, new();
        T[] Gets (IdentityId[] ids);
        Result[] Gets<Result> (IdentityId[] ids) where Result : class;
        Result[] Gets<Result> (IdentityId[] ids, Expression<Func<T, Result>> selector);
        bool Update<Set> (T sourc, Set set) where Set : class;
    }
}
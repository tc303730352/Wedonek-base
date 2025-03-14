﻿using System.Linq.Expressions;

namespace Base.FileDAL
{
    public interface IBasicDAL<T, IdentityId> where T : class, new()
    {
        bool IsExist ( Expression<Func<T, bool>> filter );
        Result[] GetAll<Result> () where Result : class, new();
        Result[] Gets<Result> ( Expression<Func<T, bool>> filter ) where Result : class, new();
        void Delete ( IdentityId id );
        T Get ( IdentityId id );
        Result Get<Result> ( IdentityId id ) where Result : class;
        Result Get<Result> ( IdentityId id, Expression<Func<T, Result>> selector );
        T[] Gets ( IdentityId[] ids );
        Result[] Gets<Result> ( IdentityId[] ids ) where Result : class;
        Result[] Gets<Result> ( IdentityId[] ids, Expression<Func<T, Result>> selector );
        bool Update<Set> ( T sourc, Set set ) where Set : class;
    }
}
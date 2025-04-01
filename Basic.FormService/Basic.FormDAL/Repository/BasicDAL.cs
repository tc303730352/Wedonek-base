using System.Linq.Expressions;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class BasicDAL<T, IdentityId> : IBasicDAL<T, IdentityId> where T : class, new()
    {
        protected readonly IRepository<T> _BasicDAL;
        private readonly string _prefix;
        public BasicDAL ( IRepository<T> basicDAL )
        {
            this._prefix = typeof(T).Name.ToLower();
            if ( this._prefix.StartsWith("db") )
            {
                this._prefix = this._prefix.Substring(2);
            }
            if ( this._prefix.EndsWith("dal") )
            {
                this._prefix = this._prefix.Substring(0, this._prefix.Length - 3);
            }
            this._BasicDAL = basicDAL;
        }
        public Result Get<Result> ( Expression<Func<T, bool>> where ) where Result : class, new()
        {
            return this._BasicDAL.Get<Result>(where);
        }
        public Result Get<Result> ( Expression<Func<T, bool>> where, Expression<Func<T, Result>> selector )
        {
            return this._BasicDAL.Get(where, selector);
        }
        public Result[] Gets<Result> ( Expression<Func<T, bool>> where ) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(where);
        }
        public Result[] Gets<Result> ( Expression<Func<T, bool>> where, Expression<Func<T, Result>> selector )
        {
            return this._BasicDAL.Gets(where, selector);
        }
        public void Delete ( IdentityId id )
        {
            if ( !this._BasicDAL.DeleteByKey(id) )
            {
                throw new ErrorException("form." + this._prefix + ".delete.fail");
            }
        }
        public void Delete ( IdentityId[] id )
        {
            if ( !this._BasicDAL.DeleteByKey(id) )
            {
                throw new ErrorException("form." + this._prefix + ".delete.fail");
            }
        }
        public T Get ( IdentityId id )
        {
            T res = this._BasicDAL.GetById(id);
            if ( res == null )
            {
                throw new ErrorException("form." + this._prefix + ".not.find");
            }
            return res;
        }
        public bool IsExists ( Expression<Func<T, bool>> where )
        {
            return this._BasicDAL.IsExist(where);
        }
        public int Count ( Expression<Func<T, bool>> where )
        {
            return this._BasicDAL.Count(where);
        }
        public Result Get<Result> ( IdentityId id, Expression<Func<T, Result>> selector )
        {
            Result res = this._BasicDAL.GetById(id, selector);
            if ( res == null )
            {
                throw new ErrorException("form." + this._prefix + ".not.find");
            }
            return res;
        }
        public Result[] GetAll<Result> () where Result : class, new()
        {
            return this._BasicDAL.GetAll<Result>();
        }
        public Result Get<Result> ( IdentityId id ) where Result : class
        {
            Result res = this._BasicDAL.GetById<IdentityId, Result>(id);
            if ( res == null )
            {
                throw new ErrorException("form." + this._prefix + ".not.find");
            }
            return res;
        }
        public T[] Gets ( IdentityId[] ids )
        {
            T[] result = this._BasicDAL.GetsById(ids);
            if ( result.IsNull() )
            {
                throw new ErrorException("form." + this._prefix + ".not.find");
            }
            return result;
        }
        public Result[] Gets<Result> ( IdentityId[] ids ) where Result : class
        {
            Result[] result = this._BasicDAL.GetsById<IdentityId, Result>(ids);
            if ( result.IsNull() )
            {
                throw new ErrorException("form." + this._prefix + ".not.find");
            }
            return result;
        }
        public Result[] Gets<Result> ( IdentityId[] ids, Expression<Func<T, Result>> selector )
        {
            Result[] result = this._BasicDAL.GetsById(ids, selector);
            if ( result.IsNull() )
            {
                throw new ErrorException("form." + this._prefix + ".not.find");
            }
            return result;
        }
        public Result[] Query<Result> ( Expression<Func<T, bool>> where, IBasicPage paging, out int count ) where Result : class
        {
            return this._BasicDAL.Query<Result>(where, paging, out count);
        }
        public bool Update<Set> ( T sourc, Set set ) where Set : class
        {
            return this._BasicDAL.Update(sourc, set);
        }
    }
}

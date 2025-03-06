using System.Linq.Expressions;
using WeDonekRpc.Helper;
using WeDonekRpc.SqlSugar;

namespace Base.FileDAL.Repository
{
    internal class BasicDAL<T, IdentityId> : IBasicDAL<T, IdentityId> where T : class, new()
    {
        protected readonly IRepository<T> _BasicDAL;
        private readonly string _prefix;
        public BasicDAL (IRepository<T> basicDAL)
        {
            this._prefix = typeof(T).Name.ToLower();
            if (this._prefix.StartsWith("db"))
            {
                this._prefix = this._prefix.Substring(2);
            }
            this._BasicDAL = basicDAL;
        }
        public void Delete (IdentityId id)
        {
            if (!this._BasicDAL.DeleteByKey(id))
            {
                throw new ErrorException("hr." + this._prefix + ".delete.fail");
            }
        }
        public T Get (IdentityId id)
        {
            T res = this._BasicDAL.GetById(id);
            if (res == null)
            {
                throw new ErrorException("hr." + this._prefix + ".not.find");
            }
            return res;
        }
        public Result Get<Result> (IdentityId id, Expression<Func<T, Result>> selector)
        {
            Result res = this._BasicDAL.GetById(id, selector);
            if (res == null)
            {
                throw new ErrorException("hr." + this._prefix + ".not.find");
            }
            return res;
        }
        public Result Get<Result> (IdentityId id) where Result : class
        {
            Result res = this._BasicDAL.GetById<IdentityId, Result>(id);
            if (res == null)
            {
                throw new ErrorException("hr." + this._prefix + ".not.find");
            }
            return res;
        }
        public T[] Gets (IdentityId[] ids)
        {
            return this._BasicDAL.GetsById(ids);
        }
        public Result[] Gets<Result> (IdentityId[] ids) where Result : class
        {
            return this._BasicDAL.GetsById<IdentityId, Result>(ids);
        }
        public Result[] Gets<Result> (IdentityId[] ids, Expression<Func<T, Result>> selector)
        {
            return this._BasicDAL.GetsById(ids, selector);
        }
        public bool Update<Set> (T sourc, Set set) where Set : class
        {
            return this._BasicDAL.Update(sourc, set);
        }
    }
}

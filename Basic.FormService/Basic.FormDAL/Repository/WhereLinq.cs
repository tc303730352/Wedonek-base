using System.Linq.Expressions;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Control.Model;
using Basic.FormRemoteModel.Form.Model;
using LinqKit;
using WeDonekRpc.Helper;

namespace Basic.FormDAL.Repository
{
    internal static class WhereLinq
    {
        public static Expression<Func<DBCustomControl, bool>> ToWhere ( this ControlQuery query )
        {
            ExpressionStarter<DBCustomControl> where = PredicateBuilder.New<DBCustomControl>();
            if ( query.QueryKey.IsNotNull() )
            {
                where = where.And(a => a.Name.Contains(query.QueryKey));
            }
            if ( query.ControlType.HasValue )
            {
                where = where.And(a => a.ControlType == query.ControlType.Value);
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
        public static Expression<Func<DBCustomForm, bool>> ToWhere(this FormQuery query)
        {
            ExpressionStarter<DBCustomForm> where = PredicateBuilder.New<DBCustomForm>(a=>a.CompanyId==query.CompanyId);
            if (query.QueryKey.IsNotNull())
            {
                where = where.And(a => a.FormName.Contains(query.QueryKey));
            }
            if (query.FormType.IsNotNull())
            {
                where = where.And(a => a.FormType == query.FormType);
            }
            if (!query.Status.IsNull())
            {
                where = where.And(a => query.Status.Contains(a.FormStatus));
            }
            return where.And(a => a.IsDrop == false);
        }
    }
}

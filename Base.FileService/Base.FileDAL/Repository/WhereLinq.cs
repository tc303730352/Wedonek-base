using System.Linq.Expressions;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using LinqKit;
using SqlSugar;
using WeDonekRpc.Helper;

namespace Base.FileDAL.Repository
{
    internal static class WhereLinq
    {
        public static Expression<Func<DBFileList, bool>> ToWhere ( this FileQuery query )
        {
            ExpressionStarter<DBFileList> where = PredicateBuilder.New<DBFileList>();
            if ( !query.FileType.IsNull() )
            {
                where = where.And(a => query.FileType.Contains(a.FileType));
            }
            if ( !query.SaveType.IsNull() )
            {
                where = where.And(a => query.SaveType.Contains(a.SaveType));
            }
            if ( query.QueryKey.IsNotNull() )
            {
                where = where.And(a => a.FileName.Contains(query.QueryKey));
            }
            if ( query.Begin.HasValue && query.End.HasValue )
            {
                where = where.And(a => SqlFunc.Between(a.SaveTime, query.Begin.Value, query.End.Value));
            }
            else if ( query.Begin.HasValue )
            {
                where = where.And(a => a.SaveTime >= query.Begin.Value);
            }
            else if ( query.End.HasValue )
            {
                where = where.And(a => a.SaveTime <= query.End.Value);
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
    }
}

using System.Linq.Expressions;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileModel.UserFileDir;
using LinqKit;
using SqlSugar;
using WeDonekRpc.Helper;

namespace Base.FileDAL.Repository
{
    internal static class WhereLinq
    {
        public static Expression<Func<DBUserFileDir, bool>> ToWhere ( this UserFileDirQuery query )
        {
            ExpressionStarter<DBUserFileDir> where = PredicateBuilder.New<DBUserFileDir>();
            if ( !query.QueryKey.IsNull() )
            {
                if ( query.QueryKey.Validate(WeDonekRpc.Helper.Validate.ValidateFormat.数字字母) )
                {
                    where = where.And(a => a.DirKey.Contains(query.QueryKey));
                }
                else
                {
                    where = where.And(a => a.DirName.Contains(query.QueryKey));
                }
            }
            if ( query.DirStatus.HasValue )
            {
                where = where.And(a => a.DirStatus == query.DirStatus.Value);
            }
            if ( query.Power.HasValue )
            {
                where = where.And(a => a.Power == query.Power.Value);
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
        public static Expression<Func<DBUserFileList, bool>> ToWhere ( this UserFileQuery query )
        {
            ExpressionStarter<DBUserFileList> where = PredicateBuilder.New<DBUserFileList>();
            if ( !query.QueryKey.IsNull() )
            {
                where = where.And(a => a.FileName.Contains(query.QueryKey));
            }
            if ( query.UserId.HasValue )
            {
                where = where.And(a => a.UserId == query.UserId.Value);
            }
            if ( query.FileType.HasValue )
            {
                where = where.And(a => a.FileType == query.FileType.Value);
            }
            if ( query.UserDirId.HasValue )
            {
                where = where.And(a => a.UserDirId == query.UserDirId.Value);
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.FileStatus));
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
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

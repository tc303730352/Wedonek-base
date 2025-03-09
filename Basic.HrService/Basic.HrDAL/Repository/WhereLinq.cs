using System.Linq.Expressions;
using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel.Company.Model;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Dic.Model;
using Basic.HrRemoteModel.DicItem.Model;
using Basic.HrRemoteModel.Emp.Model;
using Basic.HrRemoteModel.Power.Model;
using Basic.HrRemoteModel.Role.Model;
using Basic.HrRemoteModel.TreeDic.Model;
using LinqKit;
using SqlSugar;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal static class WhereLinq
    {
        public static Expression<Func<DBPowerList, bool>> ToWhere ( this PowerGetParam query, long[] subSysId )
        {
            ExpressionStarter<DBPowerList> where = PredicateBuilder.New<DBPowerList>(a => subSysId.Contains(a.SubSystemId));
            if ( query.PowerType.HasValue )
            {
                where = where.And(a => a.PowerType == query.PowerType.Value);
            }
            if ( query.IsEnable.HasValue )
            {
                where = where.And(a => a.IsEnable == query.IsEnable.Value);
            }
            return where;
        }
        public static Expression<Func<DBPowerList, bool>> ToWhere ( this PowerGetParam query, long subSysId )
        {
            ExpressionStarter<DBPowerList> where = PredicateBuilder.New<DBPowerList>(a => a.SubSystemId == subSysId);
            if ( query.PowerType.HasValue )
            {
                where = where.And(a => a.PowerType == query.PowerType.Value);
            }
            if ( query.IsEnable.HasValue )
            {
                where = where.And(a => a.IsEnable == query.IsEnable.Value);
            }
            return where;
        }
        public static Expression<Func<DBDept, bool>> ToWhere ( this DeptQueryParam query, IRepository<DBDept> repository )
        {
            ExpressionStarter<DBDept> where = PredicateBuilder.New<DBDept>(a => a.CompanyId == query.CompanyId);
            if ( query.IsUnit.HasValue )
            {
                where = where.And(a => a.IsUnit == query.IsUnit.Value);
            }
            if ( query.UnitId.HasValue )
            {
                where = where.And(a => a.UnitId == query.UnitId.Value);
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( !query.QueryKey.IsNull() )
            {
                where = where.And(a => a.DeptName.Contains(query.QueryKey));
            }
            return where.And(a => a.IsToVoid == false);
        }
        public static Expression<Func<DBDept, bool>> ToWhere ( this UnitGetParam query, IRepository<DBDept> repository )
        {
            ExpressionStarter<DBDept> where = PredicateBuilder.New<DBDept>(a => a.CompanyId == query.CompanyId);
            if ( query.ParentId.HasValue )
            {
                if ( query.IsAllChildren )
                {
                    string code = repository.Get(a => a.Id == query.ParentId.Value, a => a.LevelCode);
                    code = code + query.ParentId.Value + "|";
                    where = where.And(a => a.LevelCode.StartsWith(code));
                }
                else
                {
                    where = where.And(a => a.ParentId == query.ParentId.Value);
                }
            }
            if ( query.UnitId.HasValue )
            {
                where = where.And(a => a.UnitId == query.UnitId.Value);
            }
            if ( query.IsDept.GetValueOrDefault() )
            {
                where = where.And(a => a.IsUnit == false);
            }
            else if ( query.IsUnit.GetValueOrDefault() )
            {
                where = where.And(a => a.IsUnit == true);
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            return where.And(a => a.IsToVoid == false);
        }

        public static Expression<Func<DBPowerList, bool>> ToWhere ( this PowerQuery query )
        {
            ExpressionStarter<DBPowerList> where = PredicateBuilder.New<DBPowerList>(a => a.SubSystemId == query.SubSystemId);
            if ( query.ParentId.HasValue )
            {
                where = where.And(a => a.ParentId == query.ParentId.Value);
            }
            if ( query.IsEnable.HasValue )
            {
                where = where.And(a => a.IsEnable == query.IsEnable.Value);
            }
            if ( !query.PowerType.IsNull() )
            {
                where = where.And(a => query.PowerType.Contains(a.PowerType));
            }
            if ( query.QueryKey.IsNotNull() )
            {
                where = where.And(a => a.Name.Contains(query.QueryKey));
            }
            return where;
        }
        public static Expression<Func<DBRole, bool>> ToWhere ( this RoleGetParam param )
        {
            ExpressionStarter<DBRole> where = PredicateBuilder.New<DBRole>();
            if ( param.IsEnable.HasValue )
            {
                where = where.And(a => a.IsEnable == param.IsEnable);
            }
            if ( param.QueryKey.IsNotNull() )
            {
                where = where.And(a => a.RoleName.Contains(param.QueryKey));
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
        public static Expression<Func<DBDicList, bool>> ToWhere ( this DicQuery query )
        {
            ExpressionStarter<DBDicList> where = PredicateBuilder.New<DBDicList>();
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( query.IsSysDic.HasValue )
            {
                where = where.And(a => a.IsSysDic == query.IsSysDic.Value);
            }
            if ( query.IsTreeDic.HasValue )
            {
                where = where.And(a => a.IsTreeDic == query.IsTreeDic.Value);
            }
            if ( !query.QueryKey.IsNull() )
            {
                where = where.And(a => a.DicName.Contains(query.QueryKey));
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
        public static Expression<Func<DBDicItem, bool>> ToWhere ( this DicItemQuery query )
        {
            ExpressionStarter<DBDicItem> where = PredicateBuilder.New<DBDicItem>(a => a.DicId == query.DicId);
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.DicStatus));
            }
            if ( !query.QueryKey.IsNull() )
            {
                where = where.And(a => a.DicText.Contains(query.QueryKey));
            }
            return where;
        }
        public static Expression<Func<DBTreeDicItem, bool>> ToWhere ( this TreeItemQuery query )
        {
            ExpressionStarter<DBTreeDicItem> where = PredicateBuilder.New<DBTreeDicItem>(a => a.DicId == query.DicId);
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.DicStatus));
            }
            if ( !query.QueryKey.IsNull() )
            {
                where = where.And(a => a.DicText.Contains(query.QueryKey));
            }
            return where;
        }
        public static Expression<Func<DBEmpList, bool>> ToWhere ( this SelectGetParam query )
        {
            ExpressionStarter<DBEmpList> where;
            if ( query.IsEntry )
            {
                where = PredicateBuilder.New<DBEmpList>(a => a.CompanyId == query.CompanyId && a.DeptId == query.DeptId);
            }
            else
            {
                where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && b.DeptId == query.DeptId && b.EmpId == a.EmpId).Any());
            }
            if ( query.Post.IsNotNull() )
            {
                query.Post = "|" + query.Post + "|";
                where = where.And(a => a.PostCode.Contains(query.Post));
            }
            return where;
        }
        public static Expression<Func<DBEmpList, bool>> ToWhere ( this EmpGetParam query )
        {
            ExpressionStarter<DBEmpList> where = PredicateBuilder.New<DBEmpList>(a => a.CompanyId == query.CompanyId);
            if ( !query.DeptId.IsNull() && query.IsEntry )
            {
                where = where.And(a => query.DeptId.Contains(a.DeptId));
            }
            else
            {
                where = where.And(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && query.DeptId.Contains(b.DeptId) && b.EmpId == a.EmpId).Any());
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            return where;
        }
        public static Expression<Func<DBEmpList, bool>> ToWhere ( this EmpQuery query )
        {
            ExpressionStarter<DBEmpList> where;
            if ( !query.Title.IsNull() && !query.DeptId.IsNull() )
            {
                where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && query.DeptId.Contains(b.DeptId) && query.Title.Contains(b.TitleCode) && b.EmpId == a.EmpId).Any());
            }
            else if ( !query.Title.IsNull() && query.UnitId.HasValue )
            {
                where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && b.UnitId == query.UnitId.Value && query.Title.Contains(b.TitleCode) && b.EmpId == a.EmpId).Any());
            }
            else if ( query.UnitId.HasValue )
            {
                where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && b.UnitId == query.UnitId.Value && b.EmpId == a.EmpId).Any());
            }
            else if ( !query.Title.IsNull() )
            {
                where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && query.Title.Contains(b.TitleCode) && b.EmpId == a.EmpId).Any());
            }
            else if ( !query.IsEntry )
            {
                if ( !query.DeptId.IsNull() )
                {
                    where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && query.DeptId.Contains(b.DeptId) && b.EmpId == a.EmpId).Any());
                }
                else
                {
                    where = PredicateBuilder.New<DBEmpList>(a => SqlFunc.Subqueryable<DBEmpTitle>().Where(b => b.CompanyId == query.CompanyId && b.EmpId == a.EmpId).Any());
                }
            }
            else
            {
                where = PredicateBuilder.New<DBEmpList>(a => a.CompanyId == query.CompanyId);
                if ( !query.DeptId.IsNull() )
                {
                    where = where.And(a => query.DeptId.Contains(a.DeptId));
                }
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( !query.UserType.IsNull() )
            {
                where = where.And(a => query.UserType.Contains(a.UserType));
            }
            if ( !query.Post.IsNull() )
            {
                where = where.And(a => query.Post.Contains(a.PostCode));
            }
            if ( query.IsNoOpen.HasValue )
            {
                bool isOpen = query.IsNoOpen.Value == false;
                where = where.And(a => a.IsOpenAccount == isOpen);
            }
            if ( !query.RoleId.IsNull() )
            {
                where = where.And(a => SqlFunc.Subqueryable<DBEmpRole>().Where(c => c.EmpId == a.EmpId && query.RoleId.Contains(c.RoleId)).Any());
            }
            if ( !query.QueryKey.IsNull() )
            {
                if ( query.QueryKey.Validate(ValidateFormat.手机号) )
                {
                    where = where.And(a => a.Phone == query.QueryKey);
                }
                else if ( query.QueryKey.Validate(ValidateFormat.纯数字) )
                {
                    where = where.And(a => a.Phone.Contains(query.QueryKey));
                }
                else if ( query.QueryKey.Validate(ValidateFormat.数字字母) )
                {
                    where = where.And(a => a.EmpNo.Contains(query.QueryKey));
                }
                else
                {
                    where = where.And(a => a.EmpName.Contains(query.QueryKey));
                }
            }
            return where;
        }
        public static Expression<Func<DBDept, bool>> ToWhere ( this DeptGetParam query, IRepository<DBDept> repository )
        {
            ExpressionStarter<DBDept> where = LinqKit.PredicateBuilder.New<DBDept>(a => a.CompanyId == query.CompanyId);
            if ( query.ParentId.HasValue )
            {
                if ( query.IsAllChildren )
                {
                    string code = repository.Get(a => a.Id == query.ParentId.Value, a => a.LevelCode);
                    code = code + query.ParentId.Value + "|";
                    where = where.And(a => a.LevelCode.StartsWith(code));
                }
                else
                {
                    where = where.And(a => a.ParentId == query.ParentId.Value);
                }
            }
            if ( query.IsUnit.HasValue )
            {
                where = where.And(a => a.IsUnit == query.IsUnit.Value);
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            return where.And(a => a.IsToVoid == false);
        }
        public static Expression<Func<DBCompany, bool>> ToWhere ( this ComGetParam query, IRepository<DBCompany> repository )
        {
            ExpressionStarter<DBCompany> where = LinqKit.PredicateBuilder.New<DBCompany>();
            if ( query.ParentId.HasValue )
            {
                if ( query.IsAllChildren )
                {
                    string code = repository.Get(a => a.Id == query.ParentId.Value, a => a.LevelCode);
                    code = code + query.ParentId.Value + "|";
                    where = where.And(a => a.LevelCode.StartsWith(code));
                }
                else
                {
                    where = where.And(a => a.ParentId == query.ParentId.Value);
                }
            }
            if ( !query.Status.IsNull() )
            {
                where = where.And(a => query.Status.Contains(a.Status));
            }
            if ( !query.CompanyType.IsNull() )
            {
                where = where.And(a => query.CompanyType.Contains(a.CompanyType));
            }
            if ( where.IsStarted )
            {
                return where;
            }
            return null;
        }
    }
}

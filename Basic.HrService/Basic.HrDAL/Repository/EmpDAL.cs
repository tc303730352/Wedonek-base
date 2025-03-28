using System.Linq.Expressions;
using Basic.HrDAL.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using SqlSugar;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class EmpDAL : BasicDAL<DBEmpList, long>, IEmpDAL
    {
        public EmpDAL ( IRepository<DBEmpList> basicDAL ) : base(basicDAL)
        {
        }
        public void SetStatus ( long empId, HrEmpStatus status )
        {
            if ( !this._BasicDAL.Update(a => a.Status == status, a => a.EmpId == empId) )
            {
                throw new ErrorException("hr.emp.update.fail");
            }
        }
        public EmpSelectItem[] GetSelectItems ( SelectGetParam param )
        {
            return this._BasicDAL.Gets<EmpSelectItem>(param.ToWhere());
        }
        public Result[] GetEmps<Result> ( EmpGetParam getParam, Expression<Func<DBEmpList, Result>> selector )
        {
            return this._BasicDAL.Gets<Result>(getParam.ToWhere(), selector);
        }
        public Result[] GetEmps<Result> ( EmpGetParam getParam ) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(getParam.ToWhere());
        }
        public Result[] Query<Result> ( EmpQuery query, IBasicPage paging, out int count ) where Result : class
        {
            paging.InitOrderBy("EmpId", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
        public void CheckPhone ( string phone )
        {
            if ( this._BasicDAL.IsExist(c => c.Phone == phone) )
            {
                throw new ErrorException("hr.emp.phone.repeat");
            }
        }
        public void CheckEmpNo ( string empNo )
        {
            if ( this._BasicDAL.IsExist(c => c.EmpNo == empNo) )
            {
                throw new ErrorException("hr.emp.no.repeat");
            }
        }

        public void CheckIDCard ( string idCard )
        {
            if ( this._BasicDAL.IsExist(c => c.IDCard == idCard) )
            {
                throw new ErrorException("hr.emp.idCard.repeat");
            }
        }
        public void Add ( DBEmpList add, string[] title )
        {
            add.EmpId = IdentityHelper.CreateId();
            add.Status = HrEmpStatus.起草;
            add.RegTime = DateTime.Now;
            if ( title.IsNull() )
            {
                this._BasicDAL.Insert(add);
                return;
            }
            ISqlQueue<DBEmpList> queue = this._BasicDAL.BeginQueue();
            queue.Insert(add);
            queue.InsertBy<DBEmpTitle>(title.ConvertAll(a => new DBEmpTitle
            {
                CompanyId = add.CompanyId,
                DeptId = add.DeptId,
                EmpId = add.EmpId,
                Id = IdentityHelper.CreateId(),
                TitleCode = a
            }));
            if ( queue.Submit() < 0 )
            {
                throw new ErrorException("hr.emp.add.fail");
            }
        }
        public string[] Set ( DBEmpList soure, EmpSetDto set, string[] title )
        {
            long deptId = soure.DeptId;
            string[] cols = soure.Merge(set);
            if ( cols.IsNull() && title.IsNull() )
            {
                return cols;
            }
            ISqlQueue<DBEmpList> queue = this._BasicDAL.BeginQueue();
            if ( !cols.IsNull() )
            {
                queue.Update(soure, cols);
            }
            if ( !title.IsNull() )
            {
                queue.DeleteBy<DBEmpTitle>(a => a.EmpId == soure.EmpId && a.DeptId == deptId);
                queue.InsertBy<DBEmpTitle>(title.ConvertAll(a => new DBEmpTitle
                {
                    Id = IdentityHelper.CreateId(),
                    CompanyId = soure.CompanyId,
                    DeptId = set.DeptId,
                    EmpId = soure.EmpId,
                    TitleCode = a,
                    UnitId = set.UnitId
                }));
            }
            if ( soure.IsOpenAccount && deptId != set.DeptId )
            {
                queue.DeleteBy<DBEmpDeptPower>(a => a.EmpId == soure.EmpId && a.DeptId == deptId);
            }
            _ = queue.Submit();
            return cols;
        }
        public void SetEmpPost ( long empId, string post )
        {
            if ( !this._BasicDAL.Update(a => a.PostCode == post, a => a.EmpId == empId) )
            {
                throw new ErrorException("hr.emp.post.update.fail");
            }
        }
        public void SetUserHead ( long empId, string head )
        {
            if ( !this._BasicDAL.Update(a => a.UserHead == head, a => a.EmpId == empId) )
            {
                throw new ErrorException("hr.emp.head.update.fail");
            }
        }
        public Dictionary<long, int> GetDeptEmpNum ( long[] deptId )
        {
            return this._BasicDAL.GroupBy(a => deptId.Contains(a.DeptId), a => a.DeptId, a => new
            {
                a.DeptId,
                num = SqlFunc.AggregateCount(a.DeptId)
            }).ToDictionary(a => a.DeptId, a => a.num);
        }
        public void SetPwd ( long empId, string pwd )
        {
            if ( !this._BasicDAL.Update(a => a.EmpPwd == pwd, a => a.EmpId == empId) )
            {
                throw new ErrorException("hr.emp.pwd.update.fail");
            }
        }
        public void CheckEmail ( string email )
        {
            if ( this._BasicDAL.IsExist(c => c.Email == email) )
            {
                throw new ErrorException("hr.emp.email.repeat");
            }
        }

        public void SetEmpEntry ( DBEmpList soure, EmpEntrySet datum )
        {
            long companyId = soure.CompanyId;
            long deptId = soure.DeptId;
            string[] cols = soure.Merge(datum);
            ISqlQueue<DBEmpList> queue = this._BasicDAL.BeginQueue();
            if ( !cols.IsNull() )
            {
                queue.Update(soure, cols);
            }
            if ( soure.IsOpenAccount )
            {
                queue.DeleteBy<DBEmpDeptPower>(a => a.EmpId == soure.EmpId && a.DeptId == deptId);
            }
            if ( !datum.IsRetainTitle )
            {
                queue.DeleteBy<DBEmpTitle>(a => a.EmpId == soure.EmpId && a.CompanyId == companyId);
            }
            if ( !datum.DropTitleId.IsNull() )
            {
                queue.DeleteBy<DBEmpTitle>(a => datum.DropTitleId.Contains(a.Id));
            }
            if ( !datum.Title.IsNull() )
            {
                queue.InsertBy<DBEmpTitle>(datum.Title.ConvertAll(a => new DBEmpTitle
                {
                    Id = IdentityHelper.CreateId(),
                    CompanyId = datum.CompanyId,
                    DeptId = datum.DeptId,
                    EmpId = soure.EmpId,
                    TitleCode = a,
                    UnitId = datum.UnitId
                }));
            }
            _ = queue.Submit();
        }
    }
}

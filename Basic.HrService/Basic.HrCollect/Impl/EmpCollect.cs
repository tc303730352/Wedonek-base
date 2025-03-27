using System.Linq.Expressions;
using Basic.HrDAL;
using Basic.HrDAL.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using SqlSugar;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class EmpCollect : IEmpCollect
    {
        private readonly IEmpDAL _Emp;
        public EmpCollect ( IEmpDAL emp )
        {
            this._Emp = emp;
        }
        public bool CheckIsRepeat ( DataRepeatCheck check )
        {
            long empId;
            if ( check.CheckType == HrEmpRepeatCheckType.员工编号 )
            {
                empId = this._Emp.Get(c => c.CompanyId == check.CompanyId && c.EmpNo == check.Value, c => c.EmpId);
            }
            else if ( check.CheckType == HrEmpRepeatCheckType.手机号 )
            {
                empId = this._Emp.Get(c => c.CompanyId == check.CompanyId && c.Phone == check.Value, c => c.EmpId);
            }
            else if ( check.CheckType == HrEmpRepeatCheckType.Email )
            {
                empId = this._Emp.Get(c => c.CompanyId == check.CompanyId && c.Email == check.Value, c => c.EmpId);
            }
            else
            {
                empId = this._Emp.Get(c => c.CompanyId == check.CompanyId && c.IDCard == check.Value, c => c.EmpId);
            }
            if ( empId == 0 )
            {
                return false;
            }
            else if ( check.EmpId.HasValue )
            {
                return empId != check.EmpId.Value;
            }
            return true;
        }

        public EmpSelectItem[] GetSelectItems ( SelectGetParam param )
        {
            return this._Emp.GetSelectItems(param);
        }
        public Result[] Query<Result> ( EmpQuery query, IBasicPage paging, out int count ) where Result : class
        {
            return this._Emp.Query<Result>(query, paging, out count);
        }
        public Result[] GetEmps<Result> ( EmpGetParam getParam, Expression<Func<DBEmpList, Result>> selector )
        {
            return this._Emp.GetEmps<Result>(getParam, selector);
        }
        public Result[] GetEmps<Result> ( EmpGetParam getParam ) where Result : class, new()
        {
            return this._Emp.GetEmps<Result>(getParam);
        }
        public string[] Set ( DBEmpList emp, EmpSetDto set, string[] title )
        {
            if ( emp.Phone != set.Phone )
            {
                this._Emp.CheckPhone(set.Phone);
            }
            if ( emp.EmpNo != set.EmpNo && set.EmpNo.IsNotNull() )
            {
                this._Emp.CheckEmpNo(set.EmpNo);
            }
            if ( emp.IDCard != set.IDCard && set.IDCard.IsNotNull() )
            {
                this._Emp.CheckIDCard(set.IDCard);
            }
            if ( emp.Email != set.Email && set.Email.IsNotNull() )
            {
                this._Emp.CheckEmail(set.Email);
            }
            return this._Emp.Set(emp, set, title);
        }
        public long Add ( EmpAdd add )
        {
            this._Emp.CheckPhone(add.Phone);
            if ( add.EmpNo.IsNotNull() )
            {
                this._Emp.CheckEmpNo(add.EmpNo);
            }
            if ( add.IDCard.IsNotNull() )
            {
                this._Emp.CheckIDCard(add.IDCard);
            }
            if ( add.Email.IsNotNull() )
            {
                this._Emp.CheckEmail(add.Email);
            }
            DBEmpList db = add.ConvertMap<EmpAdd, DBEmpList>();
            this._Emp.Add(db, add.Title);
            return db.EmpId;
        }
        public T Get<T> ( long id ) where T : class
        {
            return this._Emp.Get<T>(id);
        }
        public Result Get<Result> ( long id, Expression<Func<DBEmpList, Result>> selector )
        {
            return this._Emp.Get<Result>(id, selector);
        }
        public void Delete ( DBEmpList emp )
        {
            if ( emp.Status != HrEmpStatus.起草 )
            {
                throw new ErrorException("hr.emp.not.allow.delete");
            }
            this._Emp.Delete(emp.EmpId);
        }
        public string GetName ( long id )
        {
            return this._Emp.Get(id, a => a.EmpName);
        }
        public Dictionary<long, string> GetName ( long[] ids )
        {
            if ( ids.IsNull() )
            {
                return null;
            }
            return this._Emp.Gets(ids, a => new
            {
                a.EmpId,
                a.EmpName
            }).ToDictionary(a => a.EmpId, a => a.EmpName);
        }

        public Dictionary<long, EmpBase> GetBases ( long[] ids )
        {
            return this._Emp.Gets<EmpBase>(ids).ToDictionary(a => a.EmpId, a => a);
        }
        public bool SetStatus ( long empId, HrEmpStatus status )
        {
            HrEmpStatus old = this._Emp.Get(empId, a => a.Status);
            if ( old == status )
            {
                return false;
            }
            this._Emp.SetStatus(empId, status);
            return true;
        }
        public T[] Gets<T> ( long[] empId ) where T : class
        {
            return this._Emp.Gets<T>(empId);
        }

        public void SetUserHead ( long id, string head )
        {
            this._Emp.SetUserHead(id, head);
        }

        public void SetEmpPwd ( long empId, string newPwd )
        {
            this._Emp.SetPwd(empId, newPwd);
        }

        public Dictionary<long, int> GetDeptEmpNum ( long[] deptId )
        {
            return this._Emp.GetDeptEmpNum(deptId);
        }

        public bool CheckDeptIsExists ( long[] deptId )
        {
            HrEmpStatus[] status = new HrEmpStatus[]
            {
                HrEmpStatus.启用,
                HrEmpStatus.起草
            };
            return this._Emp.IsExists(a => deptId.Contains(a.DeptId) && status.Contains(a.Status));
        }

        public void SetEmpPost ( long empId, string post )
        {
            string oldPost = this._Emp.Get(a => a.EmpId == empId, a => a.PostCode);
            if ( oldPost == post )
            {
                return;
            }
            this._Emp.SetEmpPost(empId, post);
        }

        public string[] SetEmpEntry ( DBEmpList emp, EmpEntrySet datum )
        {
            return this._Emp.SetEmpEntry(emp, datum);
        }

        public bool CheckIsNull ( long companyId )
        {
            return !this._Emp.IsExists(c => c.CompanyId == companyId);
        }
    }
}

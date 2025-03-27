using System.Linq.Expressions;
using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.EmpTitle;
using Basic.HrRemoteModel.EmpTitle.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class EmpTitleCollect : IEmpTitleCollect
    {
        private readonly IEmpTitleDAL _EmpTitle;

        public EmpTitleCollect ( IEmpTitleDAL empTitle )
        {
            this._EmpTitle = empTitle;
        }

        public long Add ( EmpTitleAdd add )
        {
            if ( this._EmpTitle.IsExists(c => c.DeptId == add.DeptId && c.EmpId == add.EmpId && c.TitleCode == add.TitleCode) )
            {
                throw new ErrorException("hr.emp.title.repeat");
            }
            DBEmpTitle title = add.ConvertMap<EmpTitleAdd, DBEmpTitle>();
            this._EmpTitle.Add(title);
            return title.Id;
        }

        public void Delete ( DBEmpTitle obj )
        {
            this._EmpTitle.Delete(obj.Id);
        }
        public long[] GetCompanyIds ( long empId )
        {
            return this._EmpTitle.Gets(a => a.EmpId == empId, a => a.CompanyId);
        }
        public DBEmpTitle Get ( long id )
        {
            return this._EmpTitle.Get(id);
        }

        public void Clear ( long empId )
        {
            this._EmpTitle.Clear(empId);
        }

        public string[] GetTitle ( long empId, long deptId )
        {
            return this._EmpTitle.Gets(a => a.EmpId == empId && a.DeptId == deptId, a => a.TitleCode);
        }
        public Result[] GetEmpTitle<Result> ( long empId ) where Result : class, new()
        {
            return this._EmpTitle.Gets<Result>(a => a.EmpId == empId);
        }
        public Result[] GetEmpTitle<Result> ( long empId, long[] companyId ) where Result : class, new()
        {
            if ( !companyId.IsNull() )
            {
                return this._EmpTitle.Gets<Result>(a => a.EmpId == empId && companyId.Contains(a.CompanyId));
            }
            return this._EmpTitle.Gets<Result>(a => a.EmpId == empId);
        }
        public Result[] GetEmpTitle<Result> ( long empId, long companyId ) where Result : class, new()
        {
            return this._EmpTitle.Gets<Result>(a => a.EmpId == empId && a.CompanyId == companyId);
        }
        public Dictionary<long, string[]> GetEmpDeptTitle ( long[] empId, long deptId )
        {
            return this._EmpTitle.Gets(a => empId.Contains(a.EmpId) && a.DeptId == deptId, a => new
            {
                a.EmpId,
                a.TitleCode
            }).GroupBy(a => a.EmpId).ToDictionary(a => a.Key, a => a.Select(c => c.TitleCode).ToArray());
        }
        public Result[] GetEmpDeptTitle<Result> ( long[] empId, long[] deptId, Expression<Func<DBEmpTitle, Result>> selector )
        {
            return this._EmpTitle.Gets<Result>(a => empId.Contains(a.EmpId) && deptId.Contains(a.DeptId), selector);
        }
        public Dictionary<long, string[]> GetEmpTitle ( KeyValuePair<long, long>[] empAndDept )
        {
            return this._EmpTitle.GetEmpTitle(empAndDept);
        }
        public EmpTitleDto[] GetEmpTitle ( long[] empId, long companyId )
        {
            return this._EmpTitle.Gets<EmpTitleDto>(a => empId.Contains(a.EmpId) && a.CompanyId == companyId);
        }

        public bool CheckIsExists ( long empId, long comId )
        {
            return this._EmpTitle.IsExists(c => c.EmpId == empId && c.CompanyId == comId);
        }
    }
}

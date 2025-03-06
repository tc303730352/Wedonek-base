using Basic.HrModel.DB;
using Basic.HrModel.EmpTitle;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class EmpTitleDAL : BasicDAL<DBEmpTitle, long>, IEmpTitleDAL
    {
        public EmpTitleDAL (IRepository<DBEmpTitle> basicDAL) : base(basicDAL)
        {
        }

        public DBEmpTitle[] Gets (long empId)
        {
            return this._BasicDAL.Gets(a => a.EmpId == empId);
        }
        public void Add (DBEmpTitle add)
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
        }
        public string[] GetTitle (long empId, long deptId)
        {
            return this._BasicDAL.Gets(a => a.EmpId == empId && a.DeptId == deptId, a => a.TitleCode);
        }
        public void Clear (long empId)
        {
            _ = this._BasicDAL.Delete(a => a.EmpId == empId);
        }

        public EmpTitleDto[] GetEmpTitle (long[] empId, long companyId)
        {
            return this._BasicDAL.Gets<EmpTitleDto>(a => empId.Contains(a.EmpId) && a.CompanyId == companyId);
        }
        public Result[] GetEmpTitle<Result> (long empId, long companyId) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(a => a.EmpId == empId && a.CompanyId == companyId);
        }
        public long[] GetCompanyIds (long empId)
        {
            return this._BasicDAL.GroupBy(a => a.EmpId == empId, a => a.CompanyId, a => a.CompanyId);
        }

        public Dictionary<long, string[]> GetEmpTitle (KeyValuePair<long, long>[] empAndDept)
        {
            return this._BasicDAL.Gets(a => empAndDept.Any(s => a.EmpId == s.Key && a.DeptId == s.Value), a => new
            {
                a.EmpId,
                a.TitleCode
            }).GroupBy(a => a.EmpId).ToDictionary(a => a.Key, a => a.Select(c => c.TitleCode).ToArray());
        }
    }
}

using Basic.HrModel.DB;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class EmpTitleDAL : BasicDAL<DBEmpTitle, long>, IEmpTitleDAL
    {
        public EmpTitleDAL ( IRepository<DBEmpTitle> basicDAL ) : base(basicDAL)
        {
        }

        public void Add ( DBEmpTitle add )
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
        }
        public void Clear ( long empId )
        {
            _ = this._BasicDAL.Delete(a => a.EmpId == empId);
        }
        public Dictionary<long, string[]> GetEmpTitle ( KeyValuePair<long, long>[] empAndDept )
        {
            return this._BasicDAL.Gets(a => empAndDept.Any(s => a.EmpId == s.Key && a.DeptId == s.Value), a => new
            {
                a.EmpId,
                a.TitleCode
            }).GroupBy(a => a.EmpId).ToDictionary(a => a.Key, a => a.Select(c => c.TitleCode).ToArray());
        }
    }
}

using Basic.HrModel.DB;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class CompanyPowerDAL : BasicDAL<DBCompanyPowerList, long>, ICompanyPowerDAL
    {
        public CompanyPowerDAL ( IRepository<DBCompanyPowerList> basicDAL ) : base(basicDAL)
        {
        }
        public void Clear ( long companyId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.CompanyId == companyId, a => a.Id);
            if ( !ids.IsNull() )
            {
                base.Delete(ids);
            }
        }
        public void ClearByPower ( long powerId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.PowerId == powerId, a => a.Id);
            if ( !ids.IsNull() )
            {
                base.Delete(ids);
            }
        }
        public void Add ( long companyId, long[] powerId )
        {
            this._BasicDAL.Insert(powerId.ConvertAll(c => new DBCompanyPowerList
            {
                CompanyId = companyId,
                Id = IdentityHelper.CreateId(),
                PowerId = c
            }));
        }
        public void Set ( long companyId, long[] powerId, long[] ids )
        {
            ISqlQueue<DBCompanyPowerList> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(powerId.ConvertAll(c => new DBCompanyPowerList
            {
                CompanyId = companyId,
                Id = IdentityHelper.CreateId(),
                PowerId = c
            }));
            _ = queue.Submit();
        }
    }
}

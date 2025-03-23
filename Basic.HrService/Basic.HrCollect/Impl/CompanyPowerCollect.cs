using Basic.HrDAL;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class CompanyPowerCollect : ICompanyPowerCollect
    {
        private readonly ICompanyPowerDAL _BasicDAL;

        public CompanyPowerCollect ( ICompanyPowerDAL basicDAL )
        {
            this._BasicDAL = basicDAL;
        }
        public void Clear ( long companyId )
        {
            this._BasicDAL.Clear(companyId);
        }
        public void ClearByPower ( long powerId )
        {
            this._BasicDAL.ClearByPower(powerId);
        }
        public long[] GetPowerIds ( long companyId )
        {
            return this._BasicDAL.Gets<long>(a => a.CompanyId == companyId, a => a.PowerId);
        }
        public bool Sync ( long companyId, long[] powerId )
        {
            var list = this._BasicDAL.Gets(a => a.CompanyId == companyId, a => new
            {
                a.Id,
                a.PowerId
            });
            if ( list.IsNull() && powerId.IsNull() )
            {
                return false;
            }
            else if ( powerId.IsNull() )
            {
                this._BasicDAL.Delete(list.ConvertAll(a => a.Id));
                return true;
            }
            else if ( list.IsNull() )
            {
                this._BasicDAL.Add(companyId, powerId);
                return true;
            }
            else if ( list.IsEqual(powerId, ( a, b ) => a.PowerId == b) )
            {
                return false;
            }
            this._BasicDAL.Set(companyId, powerId, list.ConvertAll(a => a.Id));
            return true;
        }
    }
}

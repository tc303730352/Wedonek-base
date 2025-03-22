using Basic.HrModel.DB;

namespace Basic.HrDAL
{
    public interface ICompanyPowerDAL : IBasicDAL<DBCompanyPowerList, long>
    {
        void Add ( long companyId, long[] powerId );
        void Clear ( long companyId );
        void ClearByPower ( long powerId );
        void Set ( long companyId, long[] powerId, long[] ids );
    }
}
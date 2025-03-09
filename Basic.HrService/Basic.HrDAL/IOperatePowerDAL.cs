using Basic.HrModel.DB;

namespace Basic.HrDAL
{
    public interface IOperatePowerDAL : IBasicDAL<DBOperatePower, long>
    {
        long Add ( DBOperatePower add );
        void SetIsEnable ( DBOperatePower source, bool isEnable );
    }
}
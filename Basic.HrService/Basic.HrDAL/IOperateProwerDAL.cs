using Basic.HrModel.DB;

namespace Basic.HrDAL
{
    public interface IOperateProwerDAL : IBasicDAL<DBOperatePrower, long>
    {
        long Add ( DBOperatePrower add );
        void SetIsEnable ( DBOperatePrower source, bool isEnable );
    }
}
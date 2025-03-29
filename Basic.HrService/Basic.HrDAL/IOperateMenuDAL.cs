using Basic.HrModel.DB;

namespace Basic.HrDAL
{
    public interface IOperateMenuDAL : IBasicDAL<DBOperateMenu, long>
    {
        long Add ( DBOperateMenu add );
    }
}
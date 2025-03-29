using Basic.HrModel.DB;

namespace Basic.HrDAL
{
    public interface IUserOperateLogDAL : IBasicDAL<DBUserOperateLog, long>
    {
        void Adds ( DBUserOperateLog[] logs );
    }
}
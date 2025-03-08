using Basic.HrModel.DB;
using Basic.HrModel.OperatePrower;

namespace Basic.HrDAL
{
    public interface IRoleOperateProwerDAL : IBasicDAL<DBRoleOperatePrower, long>
    {
        long Add ( DBRoleOperatePrower add );
        void Clear ( long roleId );
        void Set ( long roleId, long prowerId, OperateItem[] prowers );
    }
}
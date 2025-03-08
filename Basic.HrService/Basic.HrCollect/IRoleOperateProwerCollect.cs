using Basic.HrModel.OperatePrower;

namespace Basic.HrCollect
{
    public interface IRoleOperateProwerCollect
    {
        void Clear ( long roleId );
        long[] GetOperateId ( long roleId, long prowerId );
        string[] GetOperateVal ( long roleId );

        string[] GetOperateVal ( long[] roleId );
        void Set ( long roleId, long prowerId, OperateItem[] prowers );
    }
}
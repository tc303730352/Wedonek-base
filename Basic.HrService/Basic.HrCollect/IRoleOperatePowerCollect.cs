using Basic.HrModel.OperatePower;

namespace Basic.HrCollect
{
    public interface IRoleOperatePowerCollect
    {
        void Clear ( long roleId );
        void ClearByPowerId ( long powerId );
        long[] GetOperateId ( long roleId, long powerId );
        string[] GetOperateVal ( long roleId );

        string[] GetOperateVal ( long[] roleId );
        void Set ( long roleId, long powerId, OperateItem[] powers );
    }
}
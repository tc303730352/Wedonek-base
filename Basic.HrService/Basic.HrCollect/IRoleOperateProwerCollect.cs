using Basic.HrModel.OperatePrower;

namespace Basic.HrCollect
{
    public interface IRoleOperateProwerCollect
    {
        void Clear ( long roleId );
        long[] GetProwerId ( long roleId );
        string[] GetProwerVal ( long roleId );

        string[] GetProwerVal ( long[] roleId );
        void Set ( long roleId, OperateItem[] prowers );
    }
}
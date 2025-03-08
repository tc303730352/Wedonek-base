using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrModel.RolePrower;
using Basic.HrRemoteModel;

namespace Basic.HrCollect
{
    public interface IRoleProwerCollect
    {
        long[] GetSubSysId ( long[] roleId );
        void Clear ( long roleId );
        ProwerRouteDto[] GetPrower ( long[] roleId );
        string GetHomeUri ( long subSysId, long[] roleId );
        long[] GetProwerId ( long[] roleId, long subSysId, ProwerType prowerType );
        void Set ( DBRole role, RolePrower[] prowers );
    }
}
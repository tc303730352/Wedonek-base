using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.RolePower;

namespace Basic.HrCollect
{
    public interface IRolePowerCollect
    {
        long[] GetSubSysId ( long[] roleId );
        void Clear ( long roleId );
        PowerRouteDto[] GetPower ( long[] roleId );
        string GetHomeUri ( long subSysId, long[] roleId );
        void Set ( DBRole role, RolePower[] powers );
    }
}
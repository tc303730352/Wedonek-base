using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel;

namespace Basic.HrCollect
{
    public interface IRolePowerCollect
    {
        long[] GetSubSysId ( long[] roleId );
        void Clear ( long roleId );
        PowerRouteDto[] GetPower ( long[] roleId );
        string GetHomeUri ( long subSysId, long[] roleId );
        long[] GetPowerId ( long[] roleId, long subSysId, PowerType powerType );
        void Set ( DBRole role, RolePower[] powers );
    }
}
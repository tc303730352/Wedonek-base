using Basic.HrRemoteModel.Power.Model;

namespace Basic.HrService.Interface
{
    public interface IPowerRouteService
    {
        PowerRoute[] GetRoutes (long userId, long subSysId);
    }
}
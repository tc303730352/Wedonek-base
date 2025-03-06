using Basic.HrRemoteModel.Prower.Model;

namespace Basic.HrService.Interface
{
    public interface IProwerRouteService
    {
        ProwerRoute[] GetRoutes (long userId, long subSysId);
    }
}
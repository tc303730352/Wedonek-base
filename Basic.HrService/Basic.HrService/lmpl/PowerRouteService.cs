using Basic.HrCollect;
using Basic.HrModel.Power;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Power.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class PowerRouteService : IPowerRouteService
    {
        private readonly IPowerCollect _Power;
        private readonly IRolePowerCollect _RolePower;
        private readonly IEmpRoleCollect _EmpRole;

        public PowerRouteService ( IPowerCollect power,
            IRolePowerCollect rolePower,
            IEmpRoleCollect empRole )
        {
            this._Power = power;
            this._RolePower = rolePower;
            this._EmpRole = empRole;
        }

        public PowerRoute[] GetRoutes ( long userId, long subSysId )
        {
            long[] roleId = this._EmpRole.GetRoleId(userId);
            if ( roleId.IsNull() )
            {
                return null;
            }
            long[] powerId = this._RolePower.GetPowerId(roleId, subSysId, PowerType.menu);
            if ( powerId.IsNull() )
            {
                return null;
            }
            PowerRouteDto[] routes = this._Power.GetRoutes(subSysId);
            routes = routes.FindAll(a => powerId.Contains(a.Id));
            return routes.Convert(a => a.ParentId == 0, a => new PowerRoute
            {
                Description = a.Description,
                Icon = a.Icon,
                Name = a.Name,
                PageParam = a.PageParam,
                PagePath = a.PagePath,
                RouteName = a.RouteName,
                RoutePath = a.RoutePath,
                Children = this._GetChildren(a, routes)
            });
        }
        private PowerRoute[] _GetChildren ( PowerRouteDto route, PowerRouteDto[] list )
        {
            return list.Convert(c => c.ParentId == route.Id, a => new PowerRoute
            {
                Description = route.Description,
                RouteName = route.RouteName,
                RoutePath = route.RoutePath,
                Icon = route.Icon,
                Name = route.Name,
                PageParam = route.PageParam,
                PagePath = route.PagePath,
                Children = this._GetChildren(a, list)
            });
        }
    }
}

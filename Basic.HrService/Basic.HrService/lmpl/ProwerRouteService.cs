using Basic.HrCollect;
using Basic.HrModel.Prower;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Prower.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class ProwerRouteService : IProwerRouteService
    {
        private readonly IProwerCollect _Prower;
        private readonly IRoleProwerCollect _RolePrower;
        private readonly IEmpRoleCollect _EmpRole;

        public ProwerRouteService (IProwerCollect prower,
            IRoleProwerCollect rolePrower,
            IEmpRoleCollect empRole)
        {
            this._Prower = prower;
            this._RolePrower = rolePrower;
            this._EmpRole = empRole;
        }

        public ProwerRoute[] GetRoutes (long userId, long subSysId)
        {
            long[] roleId = this._EmpRole.GetRoleId(userId);
            if (roleId.IsNull())
            {
                return null;
            }
            long[] prowerId = this._RolePrower.GetProwerId(roleId, subSysId, ProwerType.menu);
            if (prowerId.IsNull())
            {
                return null;
            }
            ProwerRouteDto[] routes = this._Prower.GetRoutes(subSysId);
            routes = routes.FindAll(a => prowerId.Contains(a.Id));
            return routes.Convert(a => a.ParentId == 0, a => new ProwerRoute
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
        private ProwerRoute[] _GetChildren (ProwerRouteDto route, ProwerRouteDto[] list)
        {
            return list.Convert(c => c.ParentId == route.Id, a => new ProwerRoute
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

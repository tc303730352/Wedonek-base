using System.Text;
using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.SubSystem;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.EmpLogin.Model;
using Basic.HrRemoteModel.EmpRole.Model;
using Basic.HrRemoteModel.SubSystem.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class EmpLoginDatumService : IEmpLoginDatumService
    {
        private readonly IEmpCollect _Emp;
        private readonly ICompanyPowerCollect _CompanyPower;
        private readonly IRolePowerCollect _RolePower;
        private readonly ICompanyCollect _Company;
        private readonly ISubSystemCollect _SubSystem;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IPowerCollect _Power;
        public EmpLoginDatumService (
            ICompanyCollect company,
            ICompanyPowerCollect companyPower,
            IRolePowerCollect rolePower,
            IEmpRoleCollect empRole,
            IPowerCollect power,
            IEmpCollect emp,
            ISubSystemCollect subSystem )
        {
            this._Emp = emp;
            this._Power = power;
            this._Company = company;
            this._SubSystem = subSystem;
            this._CompanyPower = companyPower;
            this._RolePower = rolePower;
            this._EmpRole = empRole;
        }
        public EmpLoginDatum Get ( long empId, long companyId, long[] company )
        {
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            EmpRole[] roles = this._EmpRole.GetRoles(companyId, empId);
            long[] roleId = roles.ConvertAll(a => a.RoleId);
            long[] subId;
            PowerRouteDto[] power;
            long curSubId;
            if ( !roles.IsExists(c => c.IsAdmin) )
            {
                subId = this._RolePower.GetSubSysId(roleId);
                power = this._RolePower.GetPower(roleId);
                curSubId = subId[0];
            }
            else
            {
                long[] powerId = this._CompanyPower.GetPowerIds(companyId);
                power = this._Power.GetEnables(powerId);
                subId = power.Distinct(a => a.SubSystemId);
                curSubId = subId[0];
            }
            SubSystemDto[] subs = this._SubSystem.Gets(subId);
            SubSystemItem[] items = subs.ConvertMap<SubSystemDto, SubSystemItem>();
            items.ForEach(c =>
            {
                PowerRouteDto dto = power.Where(a => a.SubSystemId == c.Id && a.PowerType == PowerType.menu).OrderBy(a => a.LevelNum).ThenBy(a => a.Sort).FirstOrDefault();
                c.Home = new HomeSet
                {
                    Name = dto.RouteName,
                    Params = dto.PageParam?.GetValueOrDefault("params", null)
                };
            });
            int minLevel = power.Min(a => a.LevelNum);
            Dictionary<long, string> coms = this._Company.GetNames(company);
            return new EmpLoginDatum
            {
                Company = coms,
                Name = emp.EmpName,
                Head = emp.UserHead,
                SubSystem = items,
                CurSubSysId = subId[0],
                Power = subs.ToDictionary(a => a.Id, b => power.Convert(c => c.LevelNum == minLevel && c.SubSystemId == b.Id, a => new PowerRoute
                {
                    Description = a.Description,
                    Id = a.Id,
                    Icon = a.Icon,
                    Layout = a.Layout,
                    PagePath = a.PagePath,
                    RoutePath = a.RoutePath,
                    PowerType = a.PowerType,
                    Name = a.Name,
                    PageParam = a.PageParam,
                    RouteName = a.RouteName,
                    Children = power.Convert(b => b.ParentId == a.Id, this._GetPowerRoute)
                }))
            };
        }

        private PowerRoute _GetPowerRoute ( PowerRouteDto a )
        {
            return new PowerRoute
            {
                Description = a.Description,
                Id = a.Id,
                Icon = a.Icon,
                PowerType = a.PowerType,
                Layout = a.Layout,
                Name = a.Name,
                PageParam = a.PageParam,
                PagePath = a.PagePath,
                RouteName = a.RouteName,
                RoutePath = a.RoutePath
            };
        }
    }
}

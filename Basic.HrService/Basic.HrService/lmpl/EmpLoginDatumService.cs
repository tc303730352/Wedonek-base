using System.Text;
using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.SubSystem;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.EmpLogin.Model;
using Basic.HrRemoteModel.SubSystem.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class EmpLoginDatumService : IEmpLoginDatumService
    {
        private readonly IEmpCollect _Emp;
        private readonly IEmpTitleCollect _EmpTitle;
        private readonly IRolePowerCollect _RolePower;
        private readonly IRoleCollect _Role;
        private readonly ICompanyCollect _Company;
        private readonly ISubSystemCollect _SubSystem;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IPowerCollect _Power;
        public EmpLoginDatumService (
            ICompanyCollect company,
            IEmpTitleCollect empTitle,
            IRoleCollect role,
            IRolePowerCollect rolePower,
            IEmpRoleCollect empRole,
            IPowerCollect power,
            IEmpCollect emp,
            ISubSystemCollect subSystem )
        {
            this._Emp = emp;
            this._Power = power;
            this._Role = role;
            this._Company = company;
            this._SubSystem = subSystem;
            this._EmpTitle = empTitle;
            this._RolePower = rolePower;
            this._EmpRole = empRole;
        }
        public EmpLoginDatum Get ( long empId )
        {
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            long[] roleId = this._EmpRole.GetRoleId(empId);
            bool isAdmin = this._Role.CheckIsAdmin(roleId);
            long[] subId;
            PowerRouteDto[] power;
            long curSubId;
            if ( isAdmin == false )
            {
                subId = this._RolePower.GetSubSysId(roleId);
                power = this._RolePower.GetPower(roleId);
                curSubId = subId[0];
            }
            else
            {
                power = this._Power.GetEnables();
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
            Dictionary<long, string> com = this.GetEmpCompany(empId);
            int minLevel = power.Min(a => a.LevelNum);
            return new EmpLoginDatum
            {
                Company = com,
                EmpName = emp.EmpName,
                UserHead = emp.UserHead,
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
        public Dictionary<long, string> GetEmpCompany ( long empId )
        {
            long[] comId = this._EmpTitle.GetCompanyIds(empId);
            if ( comId.IsNull() )
            {
                return null;
            }
            return this._Company.GetNames(comId);
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

using System.Text;
using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Prower;
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
        private readonly IRoleProwerCollect _RolePrower;
        private readonly IRoleCollect _Role;
        private readonly ICompanyCollect _Company;
        private readonly ISubSystemCollect _SubSystem;
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IProwerCollect _Prower;
        public EmpLoginDatumService (
            ICompanyCollect company,
            IEmpTitleCollect empTitle,
            IRoleCollect role,
            IRoleProwerCollect rolePrower,
            IEmpRoleCollect empRole,
            IProwerCollect prower,
            IEmpCollect emp,
            ISubSystemCollect subSystem)
        {
            this._Emp = emp;
            this._Prower = prower;
            this._Role = role;
            this._Company = company;
            this._SubSystem = subSystem;
            this._EmpTitle = empTitle;
            this._RolePrower = rolePrower;
            this._EmpRole = empRole;
        }
        public EmpLoginDatum Get (long empId)
        {
            DBEmpList emp = this._Emp.Get<DBEmpList>(empId);
            long[] roleId = this._EmpRole.GetRoleId(empId);
            bool isAdmin = this._Role.CheckIsAdmin(roleId);
            long[] subId;
            ProwerRouteDto[] prower;
            long curSubId;
            if (isAdmin == false)
            {
                subId = this._RolePrower.GetSubSysId(roleId);
                prower = this._RolePrower.GetPrower(roleId);
                curSubId = subId[0];
            }
            else
            {
                prower = this._Prower.GetEnables();
                subId = prower.Distinct(a => a.SubSystemId);
                curSubId = subId[0];
            }
            SubSystemDto[] subs = this._SubSystem.Gets(subId);
            SubSystemItem[] items = subs.ConvertMap<SubSystemDto, SubSystemItem>();
            items.ForEach(c =>
            {
                ProwerRouteDto dto = prower.Where(a => a.SubSystemId == c.Id && a.ProwerType == ProwerType.menu).OrderBy(a => a.LevelNum).ThenBy(a => a.Sort).FirstOrDefault();
                c.Home = new HomeSet
                {
                    Name = dto.RouteName,
                    Params = dto.PageParam?.GetValueOrDefault("params", null)
                };
            });
            Dictionary<long, string> com = this.GetEmpCompany(empId);
            int minLevel = prower.Min(a => a.LevelNum);
            return new EmpLoginDatum
            {
                Company = com,
                EmpName = emp.EmpName,
                UserHead = emp.UserHead,
                SubSystem = items,
                CurSubSysId = subId[0],
                Prower = subs.ToDictionary(a => a.Id, b => prower.Convert(c => c.LevelNum == minLevel && c.SubSystemId == b.Id, a => new ProwerRoute
                {
                    Description = a.Description,
                    Id = a.Id,
                    Icon = a.Icon,
                    Layout = a.Layout,
                    PagePath = a.PagePath,
                    RoutePath = a.RoutePath,
                    ProwerType = a.ProwerType,
                    Name = a.Name,
                    PageParam = a.PageParam,
                    RouteName = a.RouteName,
                    Children = prower.Convert(b => b.ParentId == a.Id, this._GetProwerRoute)
                }))
            };
        }
        public Dictionary<long, string> GetEmpCompany (long empId)
        {
            long[] comId = this._EmpTitle.GetCompanyIds(empId);
            if (comId.IsNull())
            {
                return null;
            }
            return this._Company.GetNames(comId);
        }
        private ProwerRoute _GetProwerRoute (ProwerRouteDto a)
        {
            return new ProwerRoute
            {
                Description = a.Description,
                Id = a.Id,
                Icon = a.Icon,
                ProwerType = a.ProwerType,
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

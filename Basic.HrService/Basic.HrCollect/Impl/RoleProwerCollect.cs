using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrModel.RolePrower;
using Basic.HrRemoteModel;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class RoleProwerCollect : IRoleProwerCollect
    {
        private readonly IRoleProwerDAL _RolePrower;
        private readonly IProwerDAL _Prower;
        public RoleProwerCollect ( IRoleProwerDAL rolePrower, IProwerDAL prower )
        {
            this._RolePrower = rolePrower;
            this._Prower = prower;
        }

        public void Clear ( long roleId )
        {
            this._RolePrower.Clear(roleId);
        }
        public long[] GetSubSysId ( long[] roleId )
        {
            return this._RolePrower.GetSubSysId(roleId);
        }
        public long[] GetProwerId ( long[] roleId, long subSysId, ProwerType prowerType )
        {
            return this._RolePrower.GetProwerId(roleId, subSysId, prowerType);
        }
        public string[] GetProwerCode ( long[] roleId, ProwerType prowerType )
        {
            long[] prowerId = this._RolePrower.GetProwerId(roleId, prowerType);
            return this._Prower.Gets(a => prowerId.Contains(a.Id) && a.IsEnable, a => a.ProwerCode).Distinct();
        }
        public ProwerRouteDto[] GetPrower ( long[] roleId )
        {
            long[] prowerId = this._RolePrower.GetProwerId(roleId, ProwerType.menu);
            if ( prowerId.IsNull() )
            {
                return Array.Empty<ProwerRouteDto>();
            }
            ProwerRouteDto[] menu = this._Prower.Gets<ProwerRouteDto>(a => prowerId.Contains(a.Id) && a.IsEnable);
            List<long> dirId = new List<long>(menu.Length * 2);
            menu.ForEach(c =>
            {
                string code = c.LevelCode.Remove(c.LevelCode.Length - 1, 1).Remove(0, 1);
                code.SplitWriteLong('|', dirId);
            });
            ProwerRouteDto[] dir = this._Prower.Gets<ProwerRouteDto>(dirId.Distinct().ToArray());
            return dir.Add(menu);
        }
        public string GetHomeUri ( long systemId, long[] roleId )
        {
            long[] prowerId = this._RolePrower.GetProwerId(roleId, ProwerType.menu);
            return this._Prower.GetHomeUri(systemId, prowerId);
        }
        public void Set ( DBRole role, RolePrower[] prowers )
        {
            if ( role.IsEnable )
            {
                throw new Exception("hr.role.not.can.update");
            }
            this._RolePrower.Set(role.Id, prowers);
        }
    }
}

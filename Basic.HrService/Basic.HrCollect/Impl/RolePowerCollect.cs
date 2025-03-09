using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class RolePowerCollect : IRolePowerCollect
    {
        private readonly IRolePowerDAL _RolePower;
        private readonly IPowerDAL _Power;
        public RolePowerCollect ( IRolePowerDAL rolePower, IPowerDAL power )
        {
            this._RolePower = rolePower;
            this._Power = power;
        }

        public void Clear ( long roleId )
        {
            this._RolePower.Clear(roleId);
        }
        public long[] GetSubSysId ( long[] roleId )
        {
            return this._RolePower.GetSubSysId(roleId);
        }
        public long[] GetPowerId ( long[] roleId, long subSysId, PowerType powerType )
        {
            return this._RolePower.GetPowerId(roleId, subSysId, powerType);
        }

        public PowerRouteDto[] GetPower ( long[] roleId )
        {
            long[] powerId = this._RolePower.GetPowerId(roleId, PowerType.menu);
            if ( powerId.IsNull() )
            {
                return Array.Empty<PowerRouteDto>();
            }
            PowerRouteDto[] menu = this._Power.Gets<PowerRouteDto>(a => powerId.Contains(a.Id) && a.IsEnable);
            List<long> dirId = new List<long>(menu.Length * 2);
            menu.ForEach(c =>
            {
                string code = c.LevelCode.Remove(c.LevelCode.Length - 1, 1).Remove(0, 1);
                code.SplitWriteLong('|', dirId);
            });
            PowerRouteDto[] dir = this._Power.Gets<PowerRouteDto>(dirId.Distinct().ToArray());
            return dir.Add(menu);
        }
        public string GetHomeUri ( long systemId, long[] roleId )
        {
            long[] powerId = this._RolePower.GetPowerId(roleId, PowerType.menu);
            return this._Power.GetHomeUri(systemId, powerId);
        }
        public void Set ( DBRole role, RolePower[] powers )
        {
            if ( role.IsEnable )
            {
                throw new Exception("hr.role.not.can.update");
            }
            this._RolePower.Set(role.Id, powers);
        }
    }
}

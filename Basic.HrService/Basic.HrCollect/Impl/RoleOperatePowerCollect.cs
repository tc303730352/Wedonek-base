using Basic.HrDAL;
using Basic.HrModel.OperatePower;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class RoleOperatePowerCollect : IRoleOperatePowerCollect
    {
        private readonly IRoleOperatePowerDAL _BasicDAL;

        public RoleOperatePowerCollect ( IRoleOperatePowerDAL basicDAL )
        {
            this._BasicDAL = basicDAL;
        }

        public void Set ( long roleId, long powerId, OperateItem[] powers )
        {
            this._BasicDAL.Set(roleId, powerId, powers);
        }
        public void Clear ( long roleId )
        {
            this._BasicDAL.Clear(roleId);
        }
        public void Clear ( long roleId, long[] powerId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.PowerId);
            ids = ids.Remove(powerId);
            if ( ids.Length > 0 )
            {
                this._BasicDAL.Clear(roleId, ids);
            }
        }
        public string[] GetOperateVal ( long roleId )
        {
            return this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.OperateVal);
        }
        public string[] GetOperateVal ( long[] roleId )
        {
            return this._BasicDAL.Gets(a => roleId.Contains(a.RoleId), a => a.OperateVal).Distinct().ToArray();
        }
        public long[] GetOperateId ( long roleId, long powerId )
        {
            return this._BasicDAL.Gets(a => a.RoleId == roleId && a.PowerId == powerId, a => a.OperateId);
        }

        public void ClearByPowerId ( long powerId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.PowerId == powerId, a => a.Id);
            if ( ids.IsNull() )
            {
                return;
            }
            this._BasicDAL.Delete(ids);
        }
    }
}

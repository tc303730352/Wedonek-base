using Basic.HrDAL;
using Basic.HrModel.OperatePrower;

namespace Basic.HrCollect.Impl
{
    internal class RoleOperateProwerCollect : IRoleOperateProwerCollect
    {
        private readonly IRoleOperateProwerDAL _BasicDAL;

        public RoleOperateProwerCollect ( IRoleOperateProwerDAL basicDAL )
        {
            this._BasicDAL = basicDAL;
        }

        public void Set ( long roleId, OperateItem[] prowers )
        {
            this._BasicDAL.Set(roleId, prowers);
        }
        public void Clear ( long roleId )
        {
            this._BasicDAL.Clear(roleId);
        }
        public string[] GetProwerVal ( long roleId )
        {
            return this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.OperateVal);
        }
        public string[] GetProwerVal ( long[] roleId )
        {
            return this._BasicDAL.Gets(a => roleId.Contains(a.RoleId), a => a.OperateVal).Distinct().ToArray();
        }
        public long[] GetProwerId ( long roleId )
        {
            return this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.ProwerId);
        }
    }
}

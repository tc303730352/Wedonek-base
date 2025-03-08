using Basic.HrCollect;
using Basic.HrModel.OperatePrower;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class RoleOperateProwerService : IRoleOperateProwerService
    {
        private readonly IRoleOperateProwerCollect _Service;
        private readonly IOperateProwerCollect _Operate;

        public RoleOperateProwerService ( IRoleOperateProwerCollect service, IOperateProwerCollect operate )
        {
            this._Service = service;
            this._Operate = operate;
        }

        public long[] GetProwerId ( long roleId )
        {
            return this._Service.GetProwerId(roleId);
        }

        public string[] GetProwerVal ( long roleId )
        {
            return this._Service.GetProwerVal(roleId);
        }

        public void Set ( long roleId, long[] prowerId )
        {
            OperateItem[] items = this._Operate.Gets<OperateItem>(prowerId);
            this._Service.Set(roleId, items);
        }
    }
}

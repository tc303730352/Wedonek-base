using Basic.HrCollect;
using Basic.HrModel.OperatePower;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class RoleOperatePowerService : IRoleOperatePowerService
    {
        private readonly IRoleOperatePowerCollect _Service;
        private readonly IOperatePowerCollect _Operate;

        public RoleOperatePowerService ( IRoleOperatePowerCollect service, IOperatePowerCollect operate )
        {
            this._Service = service;
            this._Operate = operate;
        }

        public long[] GetOperateId ( long roleId, long powerId )
        {
            return this._Service.GetOperateId(roleId, powerId);
        }

        public string[] GetOperateVal ( long roleId )
        {
            return this._Service.GetOperateVal(roleId);
        }

        public void Set ( long roleId, long powerId, long[] operateId )
        {
            OperateItem[] items = null;
            if ( !operateId.IsNull() )
            {
                items = this._Operate.Gets<OperateItem>(operateId);
            }
            this._Service.Set(roleId, powerId, items);
        }
    }
}

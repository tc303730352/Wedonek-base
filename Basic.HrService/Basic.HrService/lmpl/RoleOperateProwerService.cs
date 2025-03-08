using Basic.HrCollect;
using Basic.HrModel.OperatePrower;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

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

        public long[] GetOperateId ( long roleId, long prowerId )
        {
            return this._Service.GetOperateId(roleId, prowerId);
        }

        public string[] GetOperateVal ( long roleId )
        {
            return this._Service.GetOperateVal(roleId);
        }

        public void Set ( long roleId, long prowerId, long[] operateId )
        {
            OperateItem[] items = null;
            if ( operateId.IsNull() )
            {
                items = this._Operate.Gets<OperateItem>(operateId);
            }
            this._Service.Set(roleId, prowerId, items);
        }
    }
}

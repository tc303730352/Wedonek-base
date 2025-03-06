using Basic.HrRemoteModel.DeptPrower;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class DeptProwerEvent : IRpcApiService
    {
        private readonly IDeptProwerService _Service;

        public DeptProwerEvent (IDeptProwerService service)
        {
            this._Service = service;
        }
        public long[] GetDeptPrower (GetDeptPrower obj)
        {
            return this._Service.GetDeptPrower(obj.EmpId, obj.CompanyId);
        }
        public void SetDeptPrower (SetDeptPrower set)
        {
            this._Service.SetPrower(set.EmpId, set.CompanyId, set.DeptId);
        }
    }
}

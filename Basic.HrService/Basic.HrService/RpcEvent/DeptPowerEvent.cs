using Basic.HrRemoteModel.DeptPower;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class DeptPowerEvent : IRpcApiService
    {
        private readonly IDeptPowerService _Service;

        public DeptPowerEvent ( IDeptPowerService service )
        {
            this._Service = service;
        }
        public long[] GetDeptPower ( GetDeptPower obj )
        {
            return this._Service.GetDeptPower(obj.EmpId, obj.CompanyId);
        }
        public void SetDeptPower ( SetDeptPower set )
        {
            this._Service.SetPower(set.EmpId, set.CompanyId, set.DeptId);
        }
    }
}

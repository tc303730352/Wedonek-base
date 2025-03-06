using WeDonekRpc.Client;

namespace Basic.HrLocalEvent.Model
{
    /// <summary>
    /// 用户信息变更事件
    /// </summary>
    public class UserChangeEvent : RpcLocalEvent
    {
        public UserChangeEvent (long empId)
        {
            this.EmpId = empId;
        }
        public long EmpId { get; }
    }
}

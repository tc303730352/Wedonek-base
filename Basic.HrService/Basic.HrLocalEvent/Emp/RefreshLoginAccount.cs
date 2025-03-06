using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrLocalEvent.Emp
{
    [LocalEventName("Update")]
    internal class RefreshLoginAccount : IEventHandler<EmpLocalEvent>
    {
        private readonly ILoginUserCollect _LoginUser;
        private static readonly string[] _UpdateCol = new string[]
        {
            "EmpNo",
            "Phone",
            "Email"
        };
        public RefreshLoginAccount (ILoginUserCollect loginUser)
        {
            this._LoginUser = loginUser;
        }

        public void HandleEvent (EmpLocalEvent data, string eventName)
        {
            if (!data.Emp.IsOpenAccount || !data.UpdateCol.IsExists(c => _UpdateCol.Contains(c)))
            {
                return;
            }
            this._LoginUser.SetAccount(data.Emp);
            new UserChangeEvent(data.Emp.EmpId).AsyncSend("Update");
        }
    }
}

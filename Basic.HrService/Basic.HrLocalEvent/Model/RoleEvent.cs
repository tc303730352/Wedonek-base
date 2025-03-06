using Basic.HrModel.DB;

namespace Basic.HrLocalEvent.Model
{
    public class RoleEvent : WeDonekRpc.Client.EventPublic
    {
        public RoleEvent (DBRole role)
        {
            this.Role = role;
        }
        public DBRole Role { get; }
    }
}

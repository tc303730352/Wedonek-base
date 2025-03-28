using Basic.HrModel.DB;

namespace Basic.HrLocalEvent.Model
{
    public class RoleEvent : WeDonekRpc.Client.EventPublic
    {
        public RoleEvent ( DBRole role )
        {
            this.Role = role;
        }
        public RoleEvent ( DBRole role, long[] powerId )
        {
            this.Role = role;
            this.PowerId = powerId;
        }
        public DBRole Role { get; }
        public long[] PowerId { get; }
    }
}

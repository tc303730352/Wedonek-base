using Basic.HrRemoteModel;

namespace Basic.HrModel.RolePower
{
    public class RolePower
    {
        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemId { get; set; }

        public long PowerId { get; set; }

        public PowerType PowerType { get; set; }
    }
}

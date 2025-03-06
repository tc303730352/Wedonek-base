using Basic.HrRemoteModel;

namespace Basic.HrModel.RolePrower
{
    public class RolePrower
    {
        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemId { get; set; }

        public long ProwerId { get; set; }

        public ProwerType ProwerType { get; set; }
    }
}

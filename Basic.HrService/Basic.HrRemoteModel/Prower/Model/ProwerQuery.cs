using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.Prower.Model
{
    public class ProwerQuery
    {
        /// <summary>
        /// 子系统
        /// </summary>
        [NumValidate("hr.sub.system.id.error", 1)]
        public long SubSystemId { get; set; }

        public string QueryKey { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }


        /// <summary>
        /// 菜单类型
        /// </summary>
        public ProwerType[] ProwerType { get; set; }

        public bool? IsEnable { get; set; }
    }
}

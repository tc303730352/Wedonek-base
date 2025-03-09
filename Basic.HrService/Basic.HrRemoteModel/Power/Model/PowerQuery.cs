using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerQuery
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
        public PowerType[] PowerType { get; set; }


        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}

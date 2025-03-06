using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.TreeDic
{
    /// <summary>
    /// 移动树形字典 UI参数实体
    /// </summary>
    internal class UI_MoveTreeDicItem
    {
        /// <summary>
        /// 来源
        /// </summary>
        [NumValidate("hr.tree.dic.fromId.error", 1)]
        public long FromId { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        [NumValidate("hr.tree.dic.toId.error", 1)]
        public long ToId { get; set; }

    }
}

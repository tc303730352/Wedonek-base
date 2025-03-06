using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.TreeDic
{
    /// <summary>
    /// 修改树形字典项 UI参数实体
    /// </summary>
    internal class UI_SetTreeDicItem
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
        [NumValidate("hr.tree.dic.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 树形字典项资料
        /// </summary>
        [NullValidate("hr.tree.dic.datum.null")]
        public TreeDicItemSet Datum { get; set; }

    }
}

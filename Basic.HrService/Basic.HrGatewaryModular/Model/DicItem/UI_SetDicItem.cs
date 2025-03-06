using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.DicItem
{
    /// <summary>
    /// 设置字典项 UI参数实体
    /// </summary>
    internal class UI_SetDicItem
    {
        /// <summary>
        /// 字典项ID
        /// </summary>
        [NumValidate("hr.dicitem.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 字典项资料
        /// </summary>
        [NullValidate("hr.dicitem.datum.null")]
        public DicItemSet ItemSet { get; set; }

    }
}

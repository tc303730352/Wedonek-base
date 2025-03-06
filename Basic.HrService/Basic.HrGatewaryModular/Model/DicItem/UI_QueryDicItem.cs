using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Model.DicItem
{
    /// <summary>
    /// 查询字典项 UI参数实体
    /// </summary>
    internal class UI_QueryDicItem : BasicPage
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        [NullValidate("public.query.param.null")]
        public DicItemQuery Query { get; set; }

    }
}

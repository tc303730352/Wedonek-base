using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Model.TreeDic
{
    /// <summary>
    /// 查询字典项 UI参数实体
    /// </summary>
    internal class UI_QueryTreeDicItem : BasicPage
    {
        /// <summary>
        /// 查询项
        /// </summary>
        [NullValidate("public.query.param.null")]
        public TreeItemQuery Query { get; set; }

    }
}

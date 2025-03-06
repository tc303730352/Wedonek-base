using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Model.Dic
{
    /// <summary>
    /// 查询字典 UI参数实体
    /// </summary>
    internal class UI_QueryDic : BasicPage
    {
        /// <summary>
        /// 字典查询参数
        /// </summary>
        [NullValidate("public.query.param.null")]
        public DicQuery Query { get; set; }

    }
}

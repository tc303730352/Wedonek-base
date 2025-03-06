using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Model.Prower
{
    /// <summary>
    /// 查询目录权限 UI参数实体
    /// </summary>
    internal class UI_QueryPrower : BasicPage
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        [NullValidate("public.query.param.null")]
        public ProwerQuery QueryParam { get; set; }

    }
}

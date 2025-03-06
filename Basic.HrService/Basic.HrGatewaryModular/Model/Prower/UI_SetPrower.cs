using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Prower
{
    /// <summary>
    /// 修改权限 UI参数实体
    /// </summary>
    internal class UI_SetPrower
    {
        /// <summary>
        /// 目录权限ID
        /// </summary>
        [NumValidate("hr.prower.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 目录权限资料
        /// </summary>
        [NullValidate("hr.prower.datum.null")]
        public ProwerSet Datum { get; set; }

    }
}

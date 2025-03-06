using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Dic
{
    /// <summary>
    /// 修改字典 UI参数实体
    /// </summary>
    internal class UI_SetDic
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        [NumValidate("hr.dic.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 字典资料
        /// </summary>
        [NullValidate("hr.dic.datum.null")]
        public DicSet Datum { get; set; }

    }
}

using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 获取字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDicItems : RpcRemoteArray<Model.DicItem>
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long DicId { get; set; }
    }
}

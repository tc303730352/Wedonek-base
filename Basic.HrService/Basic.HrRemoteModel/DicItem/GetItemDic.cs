using System.Collections.Frozen;
using WeDonekRpc.Client;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 批量获取字典项
    /// </summary>
    public class GetItemDic : RpcRemote<Dictionary<long, Model.DicItem[]>>
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long[] DicId { get; set; }
    }
}

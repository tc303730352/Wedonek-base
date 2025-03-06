using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 查询字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetAllDicItems : RpcRemoteArray<DicItemDto>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public DicItemQuery Query { get; set; }
    }
}

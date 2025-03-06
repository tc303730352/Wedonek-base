using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 删除字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteDicItem : RpcRemote
    {
        /// <summary>
        /// 字典项ID
        /// </summary>
        public long Id { get; set; }
    }
}

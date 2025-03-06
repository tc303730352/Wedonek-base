using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 停用字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class StopDicItem : RpcRemote
    {
        /// <summary>
        /// 字典项ID
        /// </summary>
        public long Id { get; set; }
    }
}

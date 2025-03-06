using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 启用字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class EnableDicItem : RpcRemote
    {
        /// <summary>
        /// 启用字典项ID
        /// </summary>
        public long Id { get; set; }
    }
}

using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dic
{
    /// <summary>
    /// 启用字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class EnableDic : RpcRemote<bool>
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long Id { get; set; }
    }
}

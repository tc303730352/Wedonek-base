using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dic
{
    /// <summary>
    /// 删除字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteDic : RpcRemote
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long Id { get; set; }
    }
}

using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 停用树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class StopTreeDicItem : RpcRemote<bool>
    {
        /// <summary>
        /// 树形字典ID
        /// </summary>
        public long Id { get; set; }
    }
}

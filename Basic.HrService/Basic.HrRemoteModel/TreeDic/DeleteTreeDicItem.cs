using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 删除树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteTreeDicItem : RpcRemote
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
        public long Id { get; set; }
    }
}

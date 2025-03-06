using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 启用树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class EnableTreeDicItem : RpcRemote<bool>
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
        public long Id { get; set; }
    }
}

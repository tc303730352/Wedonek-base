using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 移动树形字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class MoveTreeDicItem : RpcRemote
    {
        /// <summary>
        /// 来源
        /// </summary>
        public long FromId { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public long ToId { get; set; }
    }
}

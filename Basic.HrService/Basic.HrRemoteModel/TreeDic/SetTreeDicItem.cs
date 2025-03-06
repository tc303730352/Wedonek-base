using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 修改树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetTreeDicItem : RpcRemote<bool>
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 树形字典项资料
        /// </summary>
        public TreeDicItemSet Datum { get; set; }
    }
}

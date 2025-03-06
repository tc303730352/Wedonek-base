using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 添加树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddTreeDicItem : RpcRemote<long>
    {
        /// <summary>
        /// 树形字典资料
        /// </summary>
        public TreeDicItemAdd Datum { get; set; }
    }
}

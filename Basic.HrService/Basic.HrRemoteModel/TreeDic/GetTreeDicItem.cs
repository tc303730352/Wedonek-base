using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 获取树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetTreeDicItem : RpcRemote<TreeDicItemDto>
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
        public long Id { get; set; }
    }
}

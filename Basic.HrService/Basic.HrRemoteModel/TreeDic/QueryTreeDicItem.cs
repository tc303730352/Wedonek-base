using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 查询字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryTreeDicItem : BasicPage<TreeDicItemDto>
    {
        /// <summary>
        /// 查询项
        /// </summary>
        public TreeItemQuery Query { get; set; }
    }
}

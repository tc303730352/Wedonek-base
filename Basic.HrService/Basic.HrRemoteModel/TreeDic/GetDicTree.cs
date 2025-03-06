using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    /// <summary>
    /// 获取树形字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDicTree : RpcRemoteArray<TreeItemBase>
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long DicId { get; set; }
    }
}

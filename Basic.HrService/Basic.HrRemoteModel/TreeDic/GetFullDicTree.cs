using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    [IRemoteConfig("basic.hr.service")]
    public class GetFullDicTree : RpcRemoteArray<TreeFullItem>
    {
        /// <summary>
        /// 查询项
        /// </summary>
        public TreeItemQuery Query { get; set; }
    }
}

using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dic
{
    /// <summary>
    /// 查询字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryDic : BasicPage<DicDatum>
    {
        /// <summary>
        /// 字典查询参数
        /// </summary>
        public DicQuery Query { get; set; }
    }
}

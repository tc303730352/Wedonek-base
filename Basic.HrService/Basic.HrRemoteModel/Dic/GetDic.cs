using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dic
{
    /// <summary>
    /// 获取字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDic : RpcRemote<DicDto>
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long Id { get; set; }
    }
}

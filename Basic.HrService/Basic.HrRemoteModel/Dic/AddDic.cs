using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dic
{
    /// <summary>
    /// 新建字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddDic : RpcRemote<long>
    {
        /// <summary>
        /// 字典资料
        /// </summary>
        public DicAdd Datum { get; set; }
    }
}

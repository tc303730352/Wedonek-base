using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dic
{
    /// <summary>
    /// 修改字典
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetDic : RpcRemote<bool>
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 字典资料
        /// </summary>
        public DicSet Datum { get; set; }
    }
}

using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 添加字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddDicItem : RpcRemote<long>
    {
        /// <summary>
        /// 字典项资料
        /// </summary>
        public DicItemAdd Datum { get; set; }
    }
}

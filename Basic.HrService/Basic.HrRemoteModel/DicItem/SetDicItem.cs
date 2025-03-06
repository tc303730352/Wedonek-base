using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    /// <summary>
    /// 设置字典项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetDicItem : RpcRemote<bool>
    {
        /// <summary>
        /// 字典项ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 字典项资料
        /// </summary>
        public DicItemSet ItemSet { get; set; }
    }
}

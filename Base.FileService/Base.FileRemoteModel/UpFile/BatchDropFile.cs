using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.UpFile
{
    [IRemoteConfig("basic.file.service")]
    public class BatchDropFile : RpcRemote
    {
        /// <summary>
        /// 用户目录
        /// </summary>
        public string[] DirKey { get; set; }

        /// <summary>
        /// 关联主键
        /// </summary>
        public long[] LinkBizPk { get; set; }

    }
}

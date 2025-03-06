using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.UpFile
{
    [IRemoteConfig("basic.file.service")]
    public class SaveFile : RpcRemote
    {
        public long FileId { get; set; }

        /// <summary>
        /// 关联的PK
        /// </summary>
        public long LinkBizPk { get; set; }

        /// <summary>
        /// 删除的文件ID
        /// </summary>
        public long[] DropFileId { get; set; }
    }
}

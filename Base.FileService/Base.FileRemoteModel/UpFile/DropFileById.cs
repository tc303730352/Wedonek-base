using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.UpFile
{
    [IRemoteConfig("basic.file.service")]
    public class DropFileById : RpcRemote
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public long FileId { get; set; }

    }
}

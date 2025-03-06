using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.UpFile
{
    [IRemoteConfig("basic.file.service")]
    public class BatchDropFileById : RpcRemote
    {
        public long[] Ids { get; set; }
    }
}

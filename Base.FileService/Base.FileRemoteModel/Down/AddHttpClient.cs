using Base.FileRemoteModel.Down.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.Down
{
    [IRemoteConfig("basic.file.service")]
    public class AddHttpClient : RpcRemote
    {
        public string Scheme { get; set; }

        public HttpClientParam ClientConfig { get; set; }
    }
}

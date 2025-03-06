using Base.FileRemoteModel.Down.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.Down
{
    [IRemoteConfig("basic.file.service")]
    public class DownSmallFile : RpcRemote<DownResult>
    {
        public Uri DownUri
        {
            get;
            set;
        }
        /// <summary>
        /// 文件下载参数
        /// </summary>
        public DownFileParam Param
        {
            get;
            set;
        }
    }
}

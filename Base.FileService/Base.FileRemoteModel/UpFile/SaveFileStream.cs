using Base.FileRemoteModel.UpFile.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Base.FileRemoteModel.UpFile
{
    [IRemoteConfig("basic.file.service")]
    public class SaveFileStream : RpcRemote<FileSaveResult>
    {
        public FileSaveDatum Datum
        {
            get;
            set;
        }
        /// <summary>
        /// 文件流
        /// </summary>
        public byte[] Stream
        {
            get;
            set;
        }
    }
}

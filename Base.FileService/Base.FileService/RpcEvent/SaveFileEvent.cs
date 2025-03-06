using Base.FileRemoteModel.UpFile;
using Base.FileRemoteModel.UpFile.Model;
using Base.FileService.Interface;
using WeDonekRpc.Client.Interface;

namespace Base.FileService.RpcEvent
{
    internal class SaveFileEvent : IRpcApiService
    {
        private readonly ISaveFileService _Service;

        public SaveFileEvent (ISaveFileService service)
        {
            this._Service = service;
        }

        public FileSaveResult SaveFileStream (SaveFileStream obj)
        {
            return this._Service.SaveStream(obj.Datum, obj.Stream);
        }
    }
}

using Base.FileRemoteModel.Down;
using Base.FileRemoteModel.Down.Model;
using Base.FileService.Interface;
using WeDonekRpc.Client.Interface;

namespace Base.FileService.RpcEvent
{
    internal class DownFileEvent : IRpcApiService
    {
        private readonly IFileDownService _Service;

        public DownFileEvent (IFileDownService service)
        {
            this._Service = service;
        }

        public DownResult DownSmallFile (DownSmallFile obj)
        {
            return this._Service.DownSmallFile(obj.DownUri, obj.Param);
        }
    }
}

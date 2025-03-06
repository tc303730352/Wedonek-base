using Base.FileRemoteModel.UpFile;
using Base.FileService.Interface;
using WeDonekRpc.Client.Interface;

namespace Base.FileService.RpcEvent
{
    internal class UserFileEvent : IRpcApiService
    {
        private readonly IUserFileService _Service;

        public UserFileEvent (IUserFileService service)
        {
            this._Service = service;
        }
        public void DropFileById (DropFileById obj)
        {
            this._Service.Drop(obj.FileId);
        }
        public void BatchDropFileById (BatchDropFileById obj)
        {
            this._Service.Drop(obj.Ids);
        }
        public void BatchDropFile (BatchDropFile obj)
        {
            this._Service.Drop(obj.DirKey, obj.LinkBizPk);
        }
        public void SaveFile (SaveFile obj)
        {
            this._Service.SaveFile(obj.FileId, obj.LinkBizPk, obj.DropFileId);
        }
        public void SaveFileList (SaveFileList obj)
        {
            this._Service.SaveFile(obj.FileId, obj.LinkBizPk, obj.DropFileId);
        }
        public void DropFile (DropFile obj)
        {
            this._Service.Drop(obj.DirKey, obj.LinkBizPk, obj.Tag);
        }
    }
}

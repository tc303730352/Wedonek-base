using Base.FileCollect;
using Base.FileModel.UserFileDir;
using Base.FileRemoteModel.UpFile.Model;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.lmpl
{
    internal class SaveFileService : ISaveFileService
    {
        private readonly IUserFileDirCollect _FileDir;
        private readonly IFileService _File;
        private readonly IDirService _PhysicalDir;

        public SaveFileService ( IUserFileDirCollect fileDir,
            IFileService file,
            IDirService physicalDir )
        {
            this._FileDir = fileDir;
            this._File = file;
            this._PhysicalDir = physicalDir;
        }

        public FileSaveResult SaveStream ( FileSaveDatum obj, byte[] stream )
        {
            UserFileDirDto dir = this._FileDir.GetDir(obj.DirKey);
            return this._SaveFile(new MemoryFile(stream, obj.FileName), new UpArg
            {
                LinkBizPk = obj.LinkBizPk,
                Tag = obj.Tag,
                UserDir = dir,
                UserId = obj.UserId.GetValueOrDefault(),
                UserType = obj.UserType,
            });
        }
        private FileSaveResult _SaveFile ( IUpFile upFile, UpArg arg )
        {
            long fileSize = upFile.FileSize;
            arg.Dir = this._PhysicalDir.GetDir(ref fileSize);
            UserFile file = this._File.SaveFile(upFile, arg);
            return new FileSaveResult
            {
                FileId = file.FileId,
                FileUri = file.FileUri.AbsoluteUri
            };
        }
    }
}

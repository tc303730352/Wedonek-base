using Base.FileCollect;
using Base.FileGateway.Helper;
using Base.FileGateway.Interface;
using Base.FileGateway.Model;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileModel.UserFileDir;
using Base.FileService.Helper;
using Base.FileService.Interface;
using WeDonekRpc.Modular;

namespace Base.FileGateway.lmpl
{
    internal class FilePreUpService : IFilePreUpService
    {
        private readonly IFileCollect _Service;

        private readonly IUserFileCollect _UserFile;

        private readonly IUserFileDirCollect _UserFileDir;

        private readonly IFileConfig _Config;

        public FilePreUpService ( IFileCollect service,
            IUserFileCollect userFile,
            IUserFileDirCollect userFileDir,
            IFileConfig config )
        {
            this._Service = service;
            this._UserFile = userFile;
            this._UserFileDir = userFileDir;
            this._Config = config;
        }

        public PreUpFileResult SaveFile ( PreUpFile file, string dirKey, IUserState state )
        {
            long fileId = this._Service.FindFileId(file.FileMd5);
            if ( fileId == 0 )
            {
                return new PreUpFileResult
                {
                    IsUp = false
                };
            }
            UserFileDirDto userDir = this._UserFileDir.GetDir(dirKey);
            string ext = Path.GetExtension(file.FileName).ToLower();
            UserFileAdd add = new UserFileAdd
            {
                UserId = state.ToUserId(),
                UserType = state.ToUserType(),
                UserDirId = userDir.Id,
                Power = userDir.Power,
                PowerCode = userDir.ReadPower,
                LinkBizPk = file.LinkBizPk,
                Tag = file.Tag,
                Extension = ext,
                OperatePower = userDir.UpPower,
                FileName = file.FileName,
                FileType = ext.GetFileType(),
                FileId = fileId,
            };
            DBUserFileList userFile = this._UserFile.Add(add);
            return new PreUpFileResult
            {
                IsUp = true,
                FileId = userFile.Id,
                FileUri = userFile.GetFileUri(this._Config),
                FileName = userFile.FileName,
            };
        }
    }
}

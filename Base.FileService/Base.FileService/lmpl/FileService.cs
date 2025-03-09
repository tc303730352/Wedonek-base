using Base.FileCollect;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.lmpl
{
    internal class FileService : IFileService
    {
        private readonly IFileCollect _Service;

        private readonly IUserFileCollect _UserFile;

        private readonly IFileConfig _Config;

        public FileService (IFileCollect service,
            IUserFileCollect userFile,
            IFileConfig config)
        {
            this._UserFile = userFile;
            this._Service = service;
            this._Config = config;
        }

        public UserFile SaveFile (IUpFile file, UpArg arg)
        {
            FileDatum datum = arg.Dir.SaveFile(file);
            UserFileAdd add = new UserFileAdd
            {
                UserId = arg.UserId,
                UserType = arg.UserType,
                UserDirId = arg.UserDir.Id,
                Power = arg.UserDir.Power,
                PowerCode = arg.UserDir.ReadPower,
                LinkBizPk = arg.LinkBizPk,
                Tag = arg.Tag,
                Extension = datum.Extension,
                OperatePower = arg.UserDir.UpPower,
                FileName = file.FileName,
                FileType = datum.FileType,
                FileId = this._RegFile(datum)
            };
            DBUserFileList userFile = this._UserFile.Add(add);
            return new UserFile
            {
                FileId = userFile.Id,
                FileUri = userFile.GetFileUri(this._Config),
                FileName = datum.FileName,
            };
        }
        private long _RegFile (FileDatum add)
        {
            long fileId = this._Service.FindFileId(add.FileMd5);
            if (fileId == 0)
            {
                return this._Service.Add(add);
            }
            return fileId;
        }
    }
}

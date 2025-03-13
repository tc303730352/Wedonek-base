using Base.FileCollect;
using Base.FileGateway.FileBlockUp.Model;
using Base.FileGateway.Helper;
using Base.FileGateway.Interface;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileModel.UserFileDir;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway.FileUp.Interface;
using WeDonekRpc.HttpApiGateway.FileUp.Model;
using WeDonekRpc.HttpService.Interface;
using WeDonekRpc.Modular;

namespace Base.FileGateway.lmpl
{
    internal class BlockFileUpService : IBlockFileUpService
    {
        private readonly IFileCollect _Service;

        private readonly IUserFileCollect _UserFile;

        private readonly IUserFileDirCollect _UserFileDir;

        private readonly IDirService _PhysicalDir;

        private readonly IFileConfig _Config;

        public BlockFileUpService ( IFileCollect service,
            IUserFileCollect userFile,
            IUserFileDirCollect userFileDir,
            IDirService physicalDir,
            IFileConfig config )
        {
            this._Service = service;
            this._UserFile = userFile;
            this._UserFileDir = userFileDir;
            this._PhysicalDir = physicalDir;
            this._Config = config;
        }

        public void Init ( IBlockUp<BlockUpArg> task, IUserState state )
        {
            UpFileData<BlockUpArg> file = task.PostData;
            long fileId = this._Service.FindFileId(file.FileMd5);
            if ( fileId == 0 )
            {
                long userId = state.ToUserId();
                string taskKey = string.Concat(file.FileMd5, "_", userId).GetMd5().ToLower();
                task.BeginUp(taskKey, new BlockUpExtend
                {
                    UserId = userId,
                    UserType = state.ToUserType()
                });
                return;
            }
            UserFileDirDto userDir = this._UserFileDir.GetDir(file.Form.DirKey);
            string ext = Path.GetExtension(file.FileName).ToLower();
            UserFileAdd add = new UserFileAdd
            {
                UserId = state.ToUserId(),
                UserType = state.ToUserType(),
                UserDirId = userDir.Id,
                Power = userDir.Power,
                PowerCode = userDir.ReadPower,
                LinkBizPk = file.Form.LinkBizPk,
                Tag = file.Form.Tag,
                Extension = ext,
                OperatePower = userDir.UpPower,
                FileName = file.FileName,
                FileType = ext.GetFileType(),
                FileId = fileId,
            };
            DBUserFileList userFile = this._UserFile.Add(add);
            task.UpComplate(new UserFile
            {
                FileId = userFile.Id,
                FileUri = userFile.GetFileUri(this._Config),
                FileName = userFile.FileName,
            });
        }


        public void SaveFile ( IUpFileResult result, IUpFile file )
        {
            BlockUpArg arg = (BlockUpArg)result.File.Form;
            BlockUpExtend extend = (BlockUpExtend)result.File.Extend;
            UserFileDirDto userDir = this._UserFileDir.GetDir(arg.DirKey);
            long fileSize = result.File.FileSize;
            IDirState dir = this._PhysicalDir.GetDir(ref fileSize);
            FileDatum datum = dir.SaveFile(file);
            UserFileAdd add = new UserFileAdd
            {
                UserId = extend.UserId,
                UserType = extend.UserType,
                UserDirId = dir.Id,
                Power = userDir.Power,
                PowerCode = userDir.ReadPower,
                LinkBizPk = arg.LinkBizPk,
                Tag = arg.Tag,
                Extension = datum.Extension,
                OperatePower = userDir.UpPower,
                FileName = file.FileName,
                FileType = datum.FileType,
                FileId = this._RegFile(datum)
            };
            DBUserFileList userFile = this._UserFile.Add(add);
            result.UpComplate(new UserFile
            {
                FileId = userFile.Id,
                FileUri = userFile.GetFileUri(this._Config),
                FileName = datum.FileName,
            });
        }
        private long _RegFile ( FileDatum add )
        {
            long fileId = this._Service.FindFileId(add.FileMd5);
            if ( fileId == 0 )
            {
                return this._Service.Add(add);
            }
            return fileId;
        }
    }
}

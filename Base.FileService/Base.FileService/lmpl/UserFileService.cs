using Base.FileCollect;
using Base.FileModel.UserFile;
using Base.FileModel.UserFileDir;
using Base.FileRemoteModel;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Modular;

namespace Base.FileService.lmpl
{
    internal class UserFileService : IUserFileService
    {
        private readonly IUserFileCollect _UserFile;
        private readonly IUserFileDirCollect _UserFileDir;
        private readonly IFileConfig _Config;
        public UserFileService ( IUserFileCollect userFile, IFileConfig config, IUserFileDirCollect userFileDir )
        {
            this._Config = config;
            this._UserFile = userFile;
            this._UserFileDir = userFileDir;
        }

        public void SaveSort ( Dictionary<long, int> sort )
        {
            this._UserFile.SaveSort(sort);
        }
        public void Drop ( string dirKey, long linkBizPk, string tag )
        {
            UserFileDirDto dir = this._UserFileDir.GetDir(dirKey);
            long[] ids = this._UserFile.GetIds(dir.Id, linkBizPk, tag);
            if ( ids.IsNull() )
            {
                return;
            }
            this._UserFile.Drop(ids);
        }

        public void Delete ( long fileId, IUserState state )
        {
            UserFileDto file = this._UserFile.GetFile(fileId);
            long empId = state.GetValue<long>("EmpId");
            if ( file.UserId != empId || ( !file.OperatePower.IsNull() && !file.OperatePower.IsExists(state.Power.IsExists) ) )
            {
                throw new ErrorException("file.delete.power.error");
            }
            else if ( file.FileStatus == UserFileStatus.起草 )
            {
                this._UserFile.Delete(fileId);
                return;
            }
            else if ( file.FileStatus == UserFileStatus.停用 || file.FileStatus == UserFileStatus.已删除 )
            {
                return;
            }
            this._UserFile.Drop(fileId);
        }


        public DirUpConfig GetUploadConfig ( GetConfigArg arg )
        {
            UserFileDirDto dir = this._UserFileDir.GetDir(arg.DirKey);
            if ( !arg.LinkBizPk.HasValue )
            {
                return new DirUpConfig
                {
                    UpShow = dir.UpShow,
                    Extension = dir.AllowExtension,
                    UpFileSize = dir.AllowUpFileSize,
                    UpImgSet = dir.UpImgSet
                };
            }
            UserFileBasic[] files = this._UserFile.GetFiles(dir.Id, arg.LinkBizPk.Value, arg.Tag);
            return new DirUpConfig
            {
                Extension = dir.AllowExtension,
                UpShow = dir.UpShow,
                UpFileSize = dir.AllowUpFileSize,
                Files = files.OrderBy(a => a.Sort).ThenBy(a => a.Id).Select(a => new UserFile
                {
                    FileId = a.Id,
                    FileName = a.FileName,
                    FileUri = a.GetFileUri(this._Config)
                }).ToArray()
            };
        }
        public void SaveFile ( long[] fileId, long linkBizPk, long[] dropId )
        {
            this._UserFile.SaveFile(fileId, linkBizPk, dropId);
        }
        public void SaveFile ( long fileId, long linkBizPk, long[] dropId )
        {
            UserFileDto file = this._UserFile.GetFile(fileId);
            this._UserFile.SaveFile(file, linkBizPk, dropId);
        }
        public void Drop ( long[] fileId )
        {
            this._UserFile.Drop(fileId);
        }
        public void Drop ( long fileId )
        {
            this._UserFile.Drop(fileId);
        }

        public void Drop ( string[] dirKey, long[] linkBizPk )
        {
            long[] dirId = this._UserFileDir.GetDirId(dirKey);
            long[] ids = this._UserFile.GetIds(dirId, linkBizPk);
            if ( ids.IsNull() )
            {
                return;
            }
            this._UserFile.Drop(ids);
        }
    }
}

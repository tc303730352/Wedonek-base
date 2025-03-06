using Base.FileCollect;
using Base.FileModel.UserFileDir;
using Base.FileRemoteModel.Down.Model;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.DownFile
{
    internal class FileDownService : IFileDownService
    {
        private readonly IUserFileDirCollect _FileDir;
        private readonly IFileService _File;
        private readonly IDirService _PhysicalDir;

        public FileDownService (IUserFileDirCollect fileDir,
            IFileService file,
            IDirService physicalDir)
        {
            this._FileDir = fileDir;
            this._File = file;
            this._PhysicalDir = physicalDir;
        }

        public DownResult DownSmallFile (Uri uri, DownFileParam obj)
        {
            UserFileDirDto dir = this._FileDir.GetDir(obj.DirKey);
            HttpRequestSet req = obj.RequestSet.ToRequestSet();
            HttpResponseRes res;
            try
            {
                res = HttpTools.SendRequest(uri, obj.PostData, req);
                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return new DownResult
                    {
                        IsSuccess = false,
                        Error = "http.error." + (int)res.StatusCode
                    };
                }
            }
            catch (ErrorException e)
            {
                return new DownResult
                {
                    IsSuccess = false,
                    Error = e.ErrorCode
                };
            }
            return this._SaveFile(new HttpMemoryFile(res, obj.FileName), new UpArg
            {
                LinkBizPk = obj.LinkBizPk,
                Tag = obj.Tag,
                UserDir = dir,
                UserId = obj.UserId.GetValueOrDefault(),
                UserType = obj.UserType,
            });
        }
        private DownResult _SaveFile (IUpFile upFile, UpArg arg)
        {
            long fileSize = upFile.FileSize;
            arg.Dir = this._PhysicalDir.GetDir(ref fileSize);
            UserFile file = this._File.SaveFile(upFile, arg);
            return new DownResult
            {
                IsSuccess = true,
                FileId = file.FileId,
                FileUri = file.FileUri,
                FileName = file.FileName
            };
        }
    }
}
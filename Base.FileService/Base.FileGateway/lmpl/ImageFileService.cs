using System.Net;
using Base.FileGateway.Helper;
using Base.FileGateway.Interface;
using Base.FileModel.UserFile;
using Base.FileService.Interface;
using WeDonekRpc.Helper.Img;
using WeDonekRpc.Helper.Lock;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpApiGateway.Response;

namespace Base.FileGateway.lmpl
{
    internal class ImageFileService : IImageFileService
    {
        private readonly IFileConfig _Config;
        private readonly LockHelper _Lock = new LockHelper();

        public ImageFileService ( IFileConfig config )
        {
            this._Config = config;
        }

        public IResponse ReadImage ( UserFileDto file, bool isDown, ImgOperate operate )
        {
            FileInfo localFile = new FileInfo(file.FilePath);
            if ( !localFile.Exists )
            {
                return new HttpStatusResponse(HttpStatusCode.NotFound);
            }
            else if ( operate.CheckIsNull() )
            {
                return _GetFileResponse(localFile, file.FileName, isDown);
            }
            string path = Path.Combine(this._Config.ThumbnailCacheDir, operate.GetNewFileName(file));
            FileInfo imgFile = new FileInfo(path);
            if ( imgFile.Exists )
            {
                return _GetFileResponse(imgFile, file.FileName, isDown);
            }
            if ( this._Lock.GetLock() )
            {
                imgFile.Refresh();
                if ( !imgFile.Exists )
                {
                    Stream stream = ImageTools.ImgOperate(localFile, operate);
                    using ( FileStream fileStream = imgFile.Create() )
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    this._Lock.Exit();
                    return new StreamResponse(stream, file.FileName) { IsBinary = isDown };
                }
                this._Lock.Exit();
            }
            return _GetFileResponse(imgFile, file.FileName, isDown);
        }

        private static IResponse _GetFileResponse ( FileInfo file, string name, bool isDown )
        {
            if ( isDown )
            {
                return new StreamResponse(file, name) { IsBinary = true };
            }
            return new StreamResponse(file);
        }

        public void Dispose ()
        {
            this._Lock.Dispose();
        }
    }
}

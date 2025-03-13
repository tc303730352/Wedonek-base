using Base.FileModel.BaseFile;
using Base.FileService.Helper;
using Base.FileService.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Img;
using WeDonekRpc.Helper.Lock;

namespace Base.FileService.lmpl
{
    internal class VideoFileService : IVideoFileService
    {
        private readonly IVideoConfig _Config;
        private readonly IDirService _PhysicalDir;

        public VideoFileService ( IVideoConfig config, IDirService physicalDir )
        {
            this._Config = config;
            this._PhysicalDir = physicalDir;
        }

        public FileInfo GetThumbnail ( FileBase file )
        {
            if ( !this._Config.EnableThumbnail )
            {
                throw new ErrorException("file.thumbnail.no.enable");
            }
            IDirState dir = this._PhysicalDir.GetDir(file.DirId);
            FileInfo imgFile = new FileInfo(Path.Combine(dir.DirPath, "video", "thumbnail", file.SaveTime.ToString("yyyyMM"), file.Id + ".png"));
            if ( imgFile.Exists )
            {
                return imgFile;
            }
            return this._CreateImg(file, imgFile);
        }
        private FileInfo _CreateImg ( FileBase file, FileInfo imgFile )
        {
            string path = Path.Combine(file.DirPath, file.LocalPath);
            FileInfo source = new FileInfo(path);
            if ( !source.Exists )
            {
                throw new ErrorException("file.source.no.find");
            }
            using ( DataLock t = LockFactory.ApplyLock("video_" + file.Id) )
            {
                if ( t.GetLock() )
                {
                    imgFile.Refresh();
                    if ( !imgFile.Exists )
                    {
                        if ( !this._Config.ImgWidth.HasValue && !this._Config.ImgHeight.HasValue )
                        {
                            this._Config.CreateVideoImg(source, imgFile);
                        }
                        else
                        {
                            IDirState dir = this._PhysicalDir.GetTempDir();
                            FileInfo tImg = new FileInfo(Path.Combine(dir.DirPath, "video", "temp", file.Id + ".png"));
                            this._Config.CreateVideoImg(source, tImg);
                            using ( Stream stream = ImageTools.Resize(tImg, this._Config.ImgWidth, this._Config.ImgHeight) )
                            {
                                stream.Position = 0;
                                using ( FileStream imgF = imgFile.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read) )
                                {
                                    stream.CopyTo(imgF);
                                    imgF.Flush();
                                }
                            }
                        }
                    }
                    t.Exit();
                }
            }
            return imgFile;
        }
    }
}

using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileRemoteModel;
using Base.FileService.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.Helper
{
    internal static class FileLinq
    {
        private static readonly string[] _videoFormat = new string[] { ".mp4", ".avi", ".mkv", ".mpeg", ".mov", ".wmv", ".rm", ".rmvb" };
        private static readonly string[] _audioFormat = new string[] { ".mp3", ".wma", ".wav", ".ape", ".flac", ".ogg", ".aac" };
        private static readonly string[] _docFormat = new string[] { ".doc", ".txt", ".docx", ".xlsx", ".xls", ".ppt", ".pptx", ".crv", ",wpf", ".docm", ".dotm", ".potx", ".pptm", ".ppa", ".emf", ".dotx", ".xlw", ".odt", ".pdf", ".rtf", ".xml", ".xlsm", ".xlsb", ".xltx", ".xltm" };

        public static Uri GetFileUri (this DBUserFileList file, IFileConfig config)
        {
            string relativeUri = string.Format("file/read/{0}/{1}{2}", file.FileType.ToString(), file.Id, file.Extension);
            return new Uri(config.LocalUri, relativeUri);
        }
        public static Uri GetFileUri (this UserFileBasic file, IFileConfig config)
        {
            string relativeUri = string.Format("file/read/{0}/{1}{2}", file.FileType.ToString(), file.Id, file.Extension);
            return new Uri(config.LocalUri, relativeUri);
        }
        public static FileDatum SaveFile (this IDirState dir, IUpFile file)
        {
            string ext = Path.GetExtension(file.FileName).ToLower();
            FileDatum datum = new FileDatum
            {
                Extension = ext,
                FileSize = file.FileSize,
                FileMd5 = file.FileMd5,
                FileName = file.FileName,
                DirId = dir.Id,
                FileType = GetFileType(ext),
                DirPath = dir.DirPath,
                LocalPath = Path.Combine("file_" + ext.Remove(0, 1), "Zone_" + file.FileMd5.GetZIndex(), file.FileMd5 + ext)
            };
            string path = dir.GetFullPath(datum.LocalPath);
            if (!File.Exists(path))
            {
                _ = file.SaveFile(path);
            }
            return datum;
        }
        public static FileType GetFileType (this string ext)
        {
            if (PublicDataDic.ImgFormat.Contains(ext))
            {
                return FileType.image;
            }
            else if (_docFormat.Contains(ext))
            {
                return FileType.doc;
            }
            else if (_videoFormat.Contains(ext))
            {
                return FileType.video;
            }
            else if (_audioFormat.Contains(ext))
            {
                return FileType.audio;
            }
            return FileType.file;
        }
    }
}

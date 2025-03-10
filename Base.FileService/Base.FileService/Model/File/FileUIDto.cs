using Base.FileRemoteModel;

namespace Base.FileService.Model.File
{
    public class FileUIDto
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件MD5
        /// </summary>
        public string FileMd5 { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 保存类型
        /// </summary>
        public FileSaveType SaveType { get; set; }

        /// <summary>
        /// 文件URI
        /// </summary>
        public Uri FileUri { get; set; }

        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }
    }
}

using Base.FileRemoteModel;

namespace Base.FileModel.BaseFile
{
    public class FileDto
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
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 保存类型
        /// </summary>
        public FileSaveType SaveType { get; set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; set; }

    }
}

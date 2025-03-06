using Base.FileRemoteModel;

namespace Base.FileModel.BaseFile
{
    public class FileDatum
    {
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
        /// 存放目录ID
        /// </summary>
        public long DirId { get; set; }
        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 目录路径
        /// </summary>
        public string DirPath { get; set; }

    }
}

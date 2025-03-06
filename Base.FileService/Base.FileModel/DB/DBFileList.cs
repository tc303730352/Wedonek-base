using Base.FileRemoteModel;
using SqlSugar;

namespace Base.FileModel.DB
{
    [SqlSugar.SugarTable("FileList")]
    public class DBFileList
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id
        {
            get;
            set;
        }
        /// <summary>
        /// 目录ID
        /// </summary>
        public long DirId { get; set; }

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
        /// 目录路径
        /// </summary>
        public string DirPath { get; set; }
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
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }
    }
}

using Base.FileRemoteModel;

namespace Base.FileModel.UserFile
{
    public class UserFileBasic
    {
        public long Id { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 文件保存方式
        /// </summary>
        public FileSaveType SaveType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}

using Base.FileRemoteModel;

namespace Base.FileModel.BaseFile
{
    public class FileQuery
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string QueryKey { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType[] FileType { get; set; }

        /// <summary>
        /// 文件保存类型
        /// </summary>
        public FileSaveType[] SaveType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? Begin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? End { get; set; }
    }
}

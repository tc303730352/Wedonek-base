using Base.FileRemoteModel;

namespace Base.FileModel.UserFile
{
    public class UserFileQuery
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string QueryKey
        {
            get;
            set;
        }
        /// <summary>
        /// 上传用户ID
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType? FileType { get; set; }

        /// <summary>
        /// 用户目录ID
        /// </summary>
        public long? UserDirId { get; set; }

        /// <summary>
        /// 文件状态
        /// </summary>
        public UserFileStatus[] Status { get; set; }
    }
}

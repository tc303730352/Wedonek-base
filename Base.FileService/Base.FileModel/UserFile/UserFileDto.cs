using Base.FileRemoteModel;

namespace Base.FileModel.UserFile
{
    public class UserFileDto
    {
        /// <summary>
        /// 用户文件ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 目录Key
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户目录ID
        /// </summary>
        public long UserDirId { get; set; }

        /// <summary>
        /// 文件权限
        /// </summary>
        public FilePrower Prower { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        public string[] PowerCode { get; set; }

        /// <summary>
        /// 操作权限码
        /// </summary>
        public string[] OperatePrower { get; set; }
        /// <summary>
        /// 文件状态
        /// </summary>
        public UserFileStatus FileStatus { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        public string Extension { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

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

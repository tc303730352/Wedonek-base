using Base.FileRemoteModel;

namespace Base.FileModel.UserFile
{
    public class UserFileAdd
    {
        /// <summary>
        /// 上传用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public long FileId { get; set; }

        /// <summary>
        /// 用户目录ID
        /// </summary>
        public long UserDirId { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        public string Extension { get; set; }

        /// <summary>
        /// 关联PK键
        /// </summary>
        public long? LinkBizPk { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public FilePrower Prower { get; set; }

        public string[] PowerCode { get; set; }

        /// <summary>
        /// 操作权限码
        /// </summary>
        public string[] OperatePrower { get; set; }
        /// <summary>
        /// 标注
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
    }
}

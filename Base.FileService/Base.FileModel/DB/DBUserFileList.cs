using Base.FileRemoteModel;
using SqlSugar;

namespace Base.FileModel.DB
{
    [SqlSugar.SugarTable("UserFileList")]
    public class DBUserFileList
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 上传用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public long FileId { get; set; }


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
        /// 用户目录ID
        /// </summary>
        public long UserDirId { get; set; }

        /// <summary>
        /// 关联PK键
        /// </summary>
        public long? LinkBizPk { get; set; }


        /// <summary>
        /// 文件权限
        /// </summary>
        public FilePower Power { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        [SugarColumn(IsJson = true)]
        public string[] PowerCode { get; set; }

        /// <summary>
        /// 操作权限码
        /// </summary>
        [SugarColumn(IsJson = true)]
        public string[] OperatePower { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 文件状态
        /// </summary>
        public UserFileStatus FileStatus { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}

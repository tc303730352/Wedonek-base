using Base.FileRemoteModel;
using SqlSugar;

namespace Base.FileDAL.Model
{
    public class TUserFileDto
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
        /// 文件权限
        /// </summary>
        public FilePrower Prower { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }
        /// <summary>
        /// 权限码
        /// </summary>
        [SugarColumn(IsJson = true)]
        public string[] PowerCode { get; set; }
        /// <summary>
        /// 文件状态
        /// </summary>
        public UserFileStatus FileStatus { get; set; }

        /// <summary>
        /// 目录路径
        /// </summary>
        public string DirPath { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string ETag { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }
    }
}

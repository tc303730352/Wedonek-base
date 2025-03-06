namespace Base.FileRemoteModel.UpFile.Model
{
    public class FileSaveDatum
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 目录Key
        /// </summary>
        public string DirKey
        {
            get;
            set;
        }
        /// <summary>
        /// 关联的PK
        /// </summary>
        public long LinkBizPk { get; set; }

        /// <summary>
        /// 文件名带后缀
        /// </summary>
        public string FileName
        {
            get;
            set;
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag
        {
            get;
            set;
        }
    }
}

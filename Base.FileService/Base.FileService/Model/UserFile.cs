namespace Base.FileService.Model
{
    public class UserFile
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public long FileId
        {
            get;
            set;
        }
        /// <summary>
        /// 文件地址
        /// </summary>
        public Uri FileUri { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}

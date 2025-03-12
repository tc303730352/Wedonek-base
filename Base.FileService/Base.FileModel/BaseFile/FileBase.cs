namespace Base.FileModel.BaseFile
{
    public class FileBase
    {
        /// <summary>
        /// 文件id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

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
        public string Etag { get; set; }

        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }

        /// <summary>
        /// 获取目录ID
        /// </summary>
        public long DirId { get; set; }
    }
}

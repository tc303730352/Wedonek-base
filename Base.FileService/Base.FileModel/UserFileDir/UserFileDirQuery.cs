using Base.FileRemoteModel;

namespace Base.FileModel.UserFileDir
{
    public class UserFileDirQuery
    {
        public string QueryKey
        {
            get;
            set;
        }
        /// <summary>
        /// 目录状态
        /// </summary>
        public FileDirStatus? DirStatus { get; set; }


        /// <summary>
        /// 访问权限
        /// </summary>
        public FilePower? Power { get; set; }

    }
}

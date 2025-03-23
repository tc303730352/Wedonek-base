using Base.FileModel.UserFileDir;

namespace Base.FileService.Model
{
    public class DirUpConfig
    {
        /// <summary>
        /// 已上传文件列表
        /// </summary>
        public UserFile[] Files { get; set; }
        /// <summary>
        /// 上传说明
        /// </summary>
        public string UpShow { get; set; }

        /// <summary>
        /// 允许上传的文件大小
        /// </summary>
        public long? UpFileSize { get; set; }

        /// <summary>
        /// 允许扩展名
        /// </summary>
        public string[] Extension { get; set; }

        /// <summary>
        /// 上传图片的设置
        /// </summary>
        public UpImgSet UpImgSet { get; set; }
    }
}

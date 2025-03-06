using Base.FileRemoteModel;

namespace Base.FileModel.PhysicalDir
{
    public class PhysicalDirDto
    {
        public long Id { get; set; }


        /// <summary>
        /// 目录完整路径
        /// </summary>
        public string DirFullPath { get; set; }


        /// <summary>
        /// 目录状态
        /// </summary>
        public PhysicalDirStatus Status
        {
            get;
            set;
        }
        /// <summary>
        /// 权重值
        /// </summary>
        public int Weight { get; set; }

    }
}

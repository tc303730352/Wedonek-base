using Base.FileRemoteModel;
using SqlSugar;

namespace Base.FileModel.DB
{
    /// <summary>
    /// 物理目录
    /// </summary>
    [SugarTable("PhysicalDir")]
    public class DBPhysicalDirList
    {
        /// <summary>
        /// 文件目录ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 文件服务节点ID
        /// </summary>
        public long FileServerId
        {
            get;
            set;
        }
        /// <summary>
        /// 目录完整路径
        /// </summary>
        public string DirFullPath { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Show { get; set; }

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

        /// <summary>
        /// 当前大小
        /// </summary>
        public long CurrentSize { get; set; }
        /// <summary>
        /// 总量
        /// </summary>
        public long TotalSize { get; set; }

        public bool IsEnable { get; set; }

    }
}

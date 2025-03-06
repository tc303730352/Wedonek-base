using WeDonekRpc.HttpService.Model;

namespace Base.FileService.Interface
{
    public interface IFileConfig
    {
        /// <summary>
        /// 物理目录刷新间隔
        /// </summary>
        int EquipRefreshInterval { get; }

        /// <summary>
        /// 目录刷新间隔
        /// </summary>
        int DirRefreshInterval { get; }

        /// <summary>
        /// 文件临时授权时间(秒)
        /// </summary>
        int TempAccreditTime { get; }

        /// <summary>
        /// 文件临时授权密钥
        /// </summary>
        string FileAccreditSign { get; }
        /// <summary>
        /// 文件服务节点访问的URI地址
        /// </summary>
        Uri LocalUri { get; }
        /// <summary>
        /// 上传的文件缓存方式
        /// </summary>
        HttpCacheConfig CacheSet { get; }
        /// <summary>
        /// 缩略图缓存目录
        /// </summary>
        string ThumbnailCacheDir { get; }
    }
}
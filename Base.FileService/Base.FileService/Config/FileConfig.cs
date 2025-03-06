using Base.FileService.Interface;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Config;
using WeDonekRpc.HttpService;
using WeDonekRpc.HttpService.Model;

namespace Base.FileService.Config
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class FileConfig : IFileConfig
    {
        public FileConfig (ISysConfig config)
        {
            IConfigSection section = config.GetSection("file");
            section.AddRefreshEvent(this._Refresh);
        }

        private void _Refresh (IConfigSection section, string name)
        {
            this.EquipRefreshInterval = section.GetValue("EquipRefreshInterval", 10000);
            this.DirRefreshInterval = section.GetValue("DirRefreshInterval", 30000);
            this.TempAccreditTime = section.GetValue("TempAccreditTime", 30);
            this.FileAccreditSign = section.GetValue("FileTempSign", "6xy3#7a%ad");
            this.LocalUri = section.GetValue("LocalUri", HttpService.Config.RealRequestUri);
            this.CacheSet = section.GetValue("Cache", new HttpCacheConfig
            {
                CacheType = HttpCacheType.Public,
                SMaxAge = 9600,
                EnableEtag = true,
                MaxAge = 9600,
                MustRevalidate = true,
            });
            string dir = section.GetValue("ThumbnailCacheDir");
            if (dir.IsNull())
            {
                dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileCache", "Thumbnail");
            }
            if (!Directory.Exists(dir))
            {
                _ = Directory.CreateDirectory(dir);
            }
            this.ThumbnailCacheDir = dir;
        }
        /// <summary>
        /// 缩略图缓存目录
        /// </summary>
        public string ThumbnailCacheDir
        {
            get;
            set;
        }
        public int EquipRefreshInterval
        {
            get;
            private set;
        }
        /// <summary>
        /// 目录刷新间隔(毫秒)
        /// </summary>
        public int DirRefreshInterval
        {
            get;
            private set;
        }
        public int TempAccreditTime
        {
            get;
            private set;
        }
        /// <summary>
        /// 文件临时授权签名
        /// </summary>
        public string FileAccreditSign
        {
            get;
            private set;
        }

        public Uri LocalUri
        {
            get;
            private set;
        }
        public HttpCacheConfig CacheSet { get; set; }
    }
}

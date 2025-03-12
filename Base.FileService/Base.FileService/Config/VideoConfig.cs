using System.Runtime.InteropServices;
using Base.FileService.Interface;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Config;

namespace Base.FileService.Config
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class VideoConfig : IVideoConfig
    {

        public VideoConfig ( ISysConfig config )
        {
            IConfigSection section = config.GetSection("video");
            section.AddRefreshEvent(this._Refresh);
        }

        private void _Refresh ( IConfigSection section, string name )
        {
            bool isEnable = section.GetValue("IsEnable", false);
            if ( isEnable )
            {
                string path = section.GetValue("FFmpegPath");
                if ( path.IsNull() && RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "ffmpeg.exe");
                    if ( !File.Exists(path) )
                    {
                        isEnable = false;
                    }
                }
                else
                {
                    isEnable = false;
                }
                this.ImgWidth = section.GetValue<int?>("ImgWidth", null);
                this.ImgHeight = section.GetValue<int?>("ImgHeight", null);
                int sec = section.GetValue<int>("skipSecond", 1);
                this.SkipTime = new TimeSpan(0, 0, sec);
            }
            this.EnableThumbnail = isEnable;
        }
        public bool EnableThumbnail { get; private set; }

        public string FFmpegPath { get; private set; }

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int? ImgWidth { get; private set; }

        public int? ImgHeight { get; private set; }

        public TimeSpan SkipTime { get; private set; }
    }
}

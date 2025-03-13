using System.Diagnostics;
using Base.FileService.Interface;
using WeDonekRpc.Helper;

namespace Base.FileService.Helper
{
    public static class VideoLinq
    {
        public static void CreateVideoImg ( this IVideoConfig config, FileInfo videoFile, FileInfo imgFile )
        {
            if ( !imgFile.Directory.Exists )
            {
                imgFile.Directory.Create();
            }
            TimeSpan time = config.SkipTime;
            ProcessStartInfo start = new ProcessStartInfo(config.FFmpegPath);
            start.RedirectStandardOutput = false;
            start.RedirectStandardError = false;
            start.RedirectStandardInput = false;
            start.UseShellExecute = true;
            start.CreateNoWindow = false;
            start.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg");
            start.Arguments = string.Format("-ss {0}:{1}:{2} -i {3} -frames:v 1 {4}", time.Hours, time.Minutes, time.Seconds, videoFile.FullName, imgFile.FullName);
            using ( Process process = Process.Start(start) )
            {
                process.WaitForExit();
                imgFile.Refresh();
                if ( !imgFile.Exists )
                {
                    throw new ErrorException("file.video.thumbnail.create.fail");
                }
            }
        }
    }
}

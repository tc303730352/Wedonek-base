
namespace Base.FileService.Interface
{
    public interface IVideoConfig
    {
        bool EnableThumbnail { get; }
        string FFmpegPath { get; }
        int? ImgHeight { get; }
        int? ImgWidth { get; }
        TimeSpan SkipTime { get; }
    }
}
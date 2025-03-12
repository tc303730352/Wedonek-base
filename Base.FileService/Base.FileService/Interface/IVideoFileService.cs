using Base.FileModel.BaseFile;

namespace Base.FileService.Interface
{
    public interface IVideoFileService
    {
        FileInfo GetThumbnail ( FileBase file );
    }
}
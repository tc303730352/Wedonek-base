using Base.FileModel.BaseFile;

namespace Base.FileStoreGatewayModular.Interface
{
    public interface IVideoFileService
    {
        FileInfo GetThumbnail ( FileBase file );
    }
}
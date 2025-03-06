using Base.FileRemoteModel.UpFile.Model;

namespace Base.FileService.Interface
{
    public interface ISaveFileService
    {
        FileSaveResult SaveStream (FileSaveDatum obj, byte[] stream);
    }
}
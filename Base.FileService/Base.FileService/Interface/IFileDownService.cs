using Base.FileRemoteModel.Down.Model;

namespace Base.FileService.Interface
{
    public interface IFileDownService
    {
        DownResult DownSmallFile (Uri uri, DownFileParam obj);
    }
}
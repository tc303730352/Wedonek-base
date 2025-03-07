using Base.FileRemoteModel.Down.Model;

namespace Base.FileService.Interface
{
    public interface IFileDownService
    {
        bool AddClient ( string scheme, HttpClientParam config );
        DownResult DownSmallFile ( Uri uri, DownFileParam obj );
    }
}
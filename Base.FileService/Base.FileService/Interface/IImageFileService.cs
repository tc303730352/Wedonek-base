using Base.FileModel.UserFile;
using WeDonekRpc.Helper.Img;
using WeDonekRpc.HttpApiGateway.Interface;

namespace Base.FileService.Interface
{
    public interface IImageFileService : IDisposable
    {
        IResponse ReadImage (UserFileDto file, bool isDown, ImgOperate operate);
    }
}
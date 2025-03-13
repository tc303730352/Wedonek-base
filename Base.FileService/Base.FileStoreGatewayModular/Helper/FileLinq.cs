using Base.FileService.Interface;
using Base.FileStoreGatewayModular.Model.File;

namespace Base.FileStoreGatewayModular.Helper
{
    internal static class FileLinq
    {
        public static Uri GetFileUri ( this FileUIDto file, IFileConfig config )
        {
            string relativeUri = string.Format("file/read/{0}/{1}{2}", file.FileType.ToString(), file.Id, file.Extension);
            return new Uri(config.LocalUri, relativeUri);
        }
    }
}

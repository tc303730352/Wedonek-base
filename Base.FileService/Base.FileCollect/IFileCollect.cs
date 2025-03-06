using Base.FileModel.BaseFile;

namespace Base.FileCollect
{
    public interface IFileCollect
    {
        long Add (FileDatum add);
        long FindFileId (string md5);
    }
}
using Base.FileModel.PhysicalDir;

namespace Base.FileService.Interface
{
    public interface IDirState : IDisposable
    {
        long Id { get; }

        bool IsOnlyRead { get; }

        string DirPath { get; }

        bool Distribution (long fileSize);

        string GetFullPath (string basePath);

        string CombinePath (params string[] path);

        void Refresh (PhysicalDirDto state);
    }
}
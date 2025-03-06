using Base.FileModel.PhysicalDir;

namespace Base.FileCollect
{
    public interface IPhysicalDirCollect
    {
        PhysicalDirDto[] Gets (long serverId);
    }
}
using Base.FileModel.DB;
using Base.FileModel.PhysicalDir;

namespace Base.FileDAL
{
    public interface IPhysicalDirDAL : IBasicDAL<DBPhysicalDirList, long>
    {
        PhysicalDirDto[] Gets (long serverId);
    }
}
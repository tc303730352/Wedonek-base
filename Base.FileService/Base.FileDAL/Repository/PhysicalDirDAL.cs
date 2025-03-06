using Base.FileModel.DB;
using Base.FileModel.PhysicalDir;
using WeDonekRpc.SqlSugar;

namespace Base.FileDAL.Repository
{
    internal class PhysicalDirDAL : BasicDAL<DBPhysicalDirList, long>, IPhysicalDirDAL
    {
        public PhysicalDirDAL (IRepository<DBPhysicalDirList> basicDAL) : base(basicDAL)
        {
        }
        public PhysicalDirDto[] Gets (long serverId)
        {
            return this._BasicDAL.Gets<PhysicalDirDto>(a => a.FileServerId == serverId && a.IsEnable);
        }
    }
}

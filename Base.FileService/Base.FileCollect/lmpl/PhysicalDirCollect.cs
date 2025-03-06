using Base.FileDAL;
using Base.FileModel.PhysicalDir;
using WeDonekRpc.CacheClient.Interface;

namespace Base.FileCollect.lmpl
{
    internal class PhysicalDirCollect : IPhysicalDirCollect
    {
        private readonly ICacheController _Cache;

        private readonly IPhysicalDirDAL _PhysicalDir;

        public PhysicalDirCollect (ICacheController cache, IPhysicalDirDAL fileDir)
        {
            this._Cache = cache;
            this._PhysicalDir = fileDir;
        }
        public PhysicalDirDto[] Gets (long serverId)
        {
            string key = string.Concat("PhysicalDir_", serverId);
            if (!this._Cache.TryGet(key, out PhysicalDirDto[] dtos))
            {
                dtos = this._PhysicalDir.Gets(serverId);
                if (dtos == null)
                {
                    dtos = new PhysicalDirDto[0];
                }
                _ = this._Cache.Set(key, dtos, new TimeSpan(30, 0, 0, 0));
            }
            return dtos;
        }
    }
}

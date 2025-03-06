using Base.FileDAL;
using Base.FileModel.UserFileDir;
using WeDonekRpc.CacheClient.Interface;
using WeDonekRpc.Helper;

namespace Base.FileCollect.lmpl
{
    internal class UserFileDirCollect : IUserFileDirCollect
    {
        private readonly ICacheController _Cache;

        private readonly IUserFileDirDAL _FileDir;

        public UserFileDirCollect (ICacheController cache, IUserFileDirDAL fileDir)
        {
            this._Cache = cache;
            this._FileDir = fileDir;
        }

        public UserFileDirDto GetDir (string dirKey)
        {
            string key = string.Concat("DirCache_", dirKey);
            if (!this._Cache.TryGet(key, out UserFileDirDto dto))
            {
                dto = this._FileDir.GetByKey<UserFileDirDto>(dirKey);
                if (dto == null)
                {
                    dto = new UserFileDirDto
                    {
                        Id = 0
                    };
                    _ = this._Cache.Add(key, dto, new TimeSpan(0, 0, 30));
                }
                else
                {
                    _ = this._Cache.Set(key, dto, new TimeSpan(30, 0, 0, 0));
                }

            }
            if (dto.Id == 0)
            {
                throw new ErrorException("file.dir.not.find");
            }
            return dto;
        }
        public long[] GetDirId (string[] dirKey)
        {
            return this._FileDir.GetDirId(dirKey);
        }
    }
}

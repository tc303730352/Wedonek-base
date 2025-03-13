using Base.FileDAL;
using Base.FileModel.DB;
using Base.FileModel.UserFileDir;
using WeDonekRpc.CacheClient.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Base.FileCollect.lmpl
{
    internal class UserFileDirCollect : IUserFileDirCollect
    {
        private readonly ICacheController _Cache;

        private readonly IUserFileDirDAL _FileDir;

        public UserFileDirCollect ( ICacheController cache, IUserFileDirDAL fileDir )
        {
            this._Cache = cache;
            this._FileDir = fileDir;
        }
        public Result Get<Result> ( long id ) where Result : class, new()
        {
            return this._FileDir.Get<Result>(id);
        }
        public long Add ( UserFileDirAdd add )
        {
            if ( this._FileDir.IsExist(a => a.DirKey == add.DirKey) )
            {
                throw new ErrorException("file.dir.key.repeat");
            }
            else if ( this._FileDir.IsExist(a => a.DirName == add.DirName) )
            {
                throw new ErrorException("file.dir.name.repeat");
            }
            DBUserFileDir dir = add.ConvertMap<UserFileDirAdd, DBUserFileDir>();
            long id = this._FileDir.Add(dir);
            this._ClearCache(add.DirKey);
            return id;
        }
        public bool Set ( DBUserFileDir source, UserFileDirSet set )
        {
            if ( set.DirName != source.DirName && this._FileDir.IsExist(a => a.DirName == set.DirName) )
            {
                throw new ErrorException("file.dir.name.repeat");
            }
            if ( this._FileDir.Update(source, set) )
            {
                this._ClearCache(source.DirKey);
                return true;
            }
            return false;
        }
        private void _ClearCache ( string dirKey )
        {
            string key = string.Concat("DirCache_", dirKey);
            _ = this._Cache.Remove(key);
        }
        public Result[] GetAll<Result> () where Result : class, new()
        {
            return this._FileDir.GetAll<Result>();
        }
        public UserFileDirDto GetDir ( string dirKey )
        {
            string key = string.Concat("DirCache_", dirKey);
            if ( !this._Cache.TryGet(key, out UserFileDirDto dto) )
            {
                dto = this._FileDir.GetByKey<UserFileDirDto>(dirKey);
                if ( dto == null )
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
            if ( dto.Id == 0 )
            {
                throw new ErrorException("file.dir.not.find");
            }
            return dto;
        }
        public long[] GetDirId ( string[] dirKey )
        {
            return this._FileDir.GetDirId(dirKey);
        }

        public void Delete ( DBUserFileDir dir )
        {
            this._FileDir.Delete(dir.Id);
            this._ClearCache(dir.DirKey);
        }

        public Result[] Query<Result> ( UserFileDirQuery query, IBasicPage paging, out int count ) where Result : class, new()
        {
            return this._FileDir.Query<Result>(query, paging, out count);
        }
    }
}

using Base.FileDAL;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileRemoteModel;
using WeDonekRpc.CacheClient.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Base.FileCollect.lmpl
{
    internal class UserFileCollect : IUserFileCollect
    {
        private readonly IUserFileDAL _UserFile;
        private readonly ILocalCacheController _Cache;
        public UserFileCollect ( IUserFileDAL userFile,
            ILocalCacheController cache )
        {
            this._Cache = cache;
            this._UserFile = userFile;
        }
        public UserFileBasic[] GetFiles ( long dirId, long linkBizPk, string tag )
        {
            return this._UserFile.GetFiles(dirId, linkBizPk, tag);
        }
        public UserFileDto GetFile ( long id )
        {
            string key = "UserFile_" + id;
            if ( !this._Cache.TryGet(key, out UserFileDto dto) )
            {
                dto = this._UserFile.GetFile(id);
                if ( dto == null )
                {
                    dto = new UserFileDto { Id = 0, FileStatus = UserFileStatus.已删除 };
                    _ = this._Cache.Add(key, dto, new TimeSpan(0, 0, 30));
                    return dto;
                }
                _ = this._Cache.Set(key, dto, new TimeSpan(30, 0, 0, 0));
            }
            return dto;
        }
        public DBUserFileList Add ( UserFileAdd add )
        {
            DBUserFileList db = add.ConvertMap<UserFileAdd, DBUserFileList>();
            db.FileStatus = UserFileStatus.起草;
            this._UserFile.Add(db);
            return db;
        }
        public void SaveSort ( Dictionary<long, int> sort )
        {
            this._UserFile.SaveFile(sort);
        }
        public void SaveFile ( long[] fileId, long linkBizPk, long[] dropId )
        {
            this._UserFile.SaveFile(fileId, linkBizPk, dropId);
            fileId.ForEach(c =>
            {
                string key = "UserFile_" + c;
                _ = this._Cache.Remove(key);
            });
            dropId?.ForEach(c =>
                {
                    string key = "UserFile_" + c;
                    _ = this._Cache.Remove(key);
                });
        }
        public void SaveFile ( UserFileDto file, long linkBizPk, long[] dropId )
        {
            if ( file.FileStatus != UserFileStatus.起草 )
            {
                throw new ErrorException("file.already.save");
            }
            this._UserFile.SaveFile(file.Id, linkBizPk, dropId);
            string key = "UserFile_" + file.Id;
            _ = this._Cache.Remove(key);
            dropId?.ForEach(c =>
            {
                string key = "UserFile_" + c;
                _ = this._Cache.Remove(key);
            });
        }
        public long[] GetIds ( long dirId, long linkBikzPk, string tag )
        {
            return this._UserFile.GetIds(dirId, linkBikzPk, tag);
        }
        public long[] GetIds ( long[] dirId, long[] linkBikzPk )
        {
            return this._UserFile.GetIds(dirId, linkBikzPk);
        }

        public void Drop ( long[] ids )
        {
            this._UserFile.Drop(ids);
            ids.ForEach(c =>
            {
                string key = "UserFile_" + c;
                _ = this._Cache.Remove(key);
            });
        }
        public void Drop ( long id )
        {
            this._UserFile.Drop(id);
            string key = "UserFile_" + id;
            _ = this._Cache.Remove(key);
        }
        public void Delete ( long id )
        {
            this._UserFile.Delete(id);
            string key = "UserFile_" + id;
            _ = this._Cache.Remove(key);
        }

        public Dictionary<long, int> GetFileNum ( long[] fileId )
        {
            return this._UserFile.GetFileNum(fileId);
        }

        public Dictionary<long, int> GetDirFileNum ( long[] dirId )
        {
            return this._UserFile.GetDirFileNum(dirId);
        }

        public bool CheckIsNullDir ( long dirId )
        {
            return this._UserFile.IsExist(c => c.UserDirId == dirId) == false;
        }
    }
}

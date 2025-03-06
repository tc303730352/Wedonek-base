using Base.FileDAL;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using WeDonekRpc.CacheClient.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Base.FileCollect.lmpl
{
    internal class FileCollect : IFileCollect
    {
        private readonly IFileDAL _File;
        private readonly ICacheController _Cache;

        public FileCollect (IFileDAL file, ICacheController cache)
        {
            this._File = file;
            this._Cache = cache;
        }

        public long Add (FileDatum add)
        {
            DBFileList file = add.ConvertMap<FileDatum, DBFileList>();
            file.Etag = Tools.GetRandomStr(6);
            this._File.Add(file);
            this._RefreshCache(file);
            return file.Id;
        }
        private void _RefreshCache (DBFileList file)
        {
            string key = "File_" + file.FileMd5;
            _ = this._Cache.Set(key, file.Id);
        }
        public long FindFileId (string md5)
        {
            string key = "FileId_" + md5;
            if (!this._Cache.TryGet(key, out long fileId))
            {
                fileId = this._File.FindFileId(md5);
                if (fileId == 0)
                {
                    _ = this._Cache.Add(key, fileId, new TimeSpan(0, 0, 30));
                    return fileId;
                }
                _ = this._Cache.Set(key, fileId);
            }
            return fileId;
        }
    }
}

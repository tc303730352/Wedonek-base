using Base.FileCollect;
using Base.FileModel.BaseFile;
using Base.FileService.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpApiGateway.Model;
using WeDonekRpc.HttpApiGateway.Response;
using WeDonekRpc.HttpService.Model;

namespace Base.FileStoreGatewayModular.VisitEvent
{
    internal class FileVisitEvent : IApiServiceEvent
    {
        private readonly IFileCollect _Service;
        private readonly IFileConfig _Config;
        private static readonly string[] _FilePower = new string[] { "file.visit.power", "all" };
        public FileVisitEvent ( IFileCollect service, IFileConfig config )
        {
            this._Config = config;
            this._Service = service;
        }
        private FileBase _File;

        public void CheckAccredit ( IApiService service, ApiAccreditSet source )
        {
            service.CheckAccredit(_FilePower);
        }

        public bool CheckCache ( IApiService service, string etag, string toUpdateTime )
        {
            if ( etag.IsNotNull() )
            {
                return this._File.Etag == etag;
            }
            else if ( toUpdateTime.IsNotNull() && DateTime.TryParse(toUpdateTime, out DateTime time) )
            {
                return time == this._File.SaveTime;
            }
            return false;
        }

        public void Dispose ()
        {
        }

        public void EndRequest ( IApiService service )
        {
        }

        public void InitIdentity ( IApiService service )
        {

        }

        public void InitRequest ( IApiService service )
        {
            Uri uri = service.Request.Url;
            string id = Path.GetFileNameWithoutExtension(uri.LocalPath);
            if ( id.IsNull() )
            {
                throw new ErrorException("file.id.null");
            }
            else if ( !long.TryParse(id, out long fileId) )
            {
                throw new ErrorException("file.id.error");
            }
            else
            {
                this._File = this._Service.Get<FileBase>(fileId);
                service.RequestState.Set("file", this._File);
            }
        }

        public void ReplyEvent ( IApiService service, IResponse response )
        {
            if ( this._File == null )
            {
                return;
            }
            if ( response is StreamResponse stream && !stream.IsBinary )
            {
                HttpCacheConfig cache = this._Config.CacheSet;
                if ( cache.CacheType != HttpCacheType.NoStore && cache.CacheType != HttpCacheType.Private )
                {
                    stream.Cache = new HttpCacheSet
                    {
                        CacheType = cache.CacheType,
                        SMaxAge = cache.SMaxAge,
                        MaxAge = cache.MaxAge,
                        MustRevalidate = cache.MustRevalidate,
                        Etag = this._File.Etag,
                        LastModified = this._File.SaveTime
                    };
                }
                else
                {
                    stream.Cache = new HttpCacheSet
                    {
                        CacheType = cache.CacheType
                    };
                }
            }
        }
    }
}
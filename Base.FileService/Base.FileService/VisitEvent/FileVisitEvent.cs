using Base.FileCollect;
using Base.FileModel.UserFile;
using Base.FileRemoteModel;
using Base.FileService.Helper;
using Base.FileService.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpApiGateway.Model;
using WeDonekRpc.HttpApiGateway.Response;
using WeDonekRpc.HttpService.Model;

namespace Base.FileService.VisitEvent
{
    internal class FileVisitEvent : IApiServiceEvent
    {
        private readonly IUserFileCollect _Service;
        private readonly IFileConfig _Config;

        public FileVisitEvent ( IUserFileCollect service, IFileConfig config )
        {
            this._Config = config;
            this._Service = service;
        }
        private UserFileDto _UserFile;

        public void CheckAccredit ( IApiService service, ApiAccreditSet source )
        {
            if ( this._UserFile.Power == FilePower.公共 )
            {
                return;
            }
            service.CheckAccredit();
            if ( this._UserFile.Power == FilePower.共享 )
            {
                return;
            }
            else if ( this._UserFile.Power == FilePower.指定权限 && service.UserState.CheckPower(this._UserFile.PowerCode) )
            {
                return;
            }
            else if ( this._UserFile.UserId == service.UserState.GetValue<long>("EmpId") )
            {
                return;
            }
            throw new ErrorException("accredit.no.power");
        }

        public bool CheckCache ( IApiService service, string etag, string toUpdateTime )
        {
            if ( etag.IsNotNull() )
            {
                return this._UserFile.Etag == etag;
            }
            else if ( toUpdateTime.IsNotNull() && DateTime.TryParse(toUpdateTime, out DateTime time) )
            {
                return time == this._UserFile.SaveTime;
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
                this._UserFile = this._Service.GetFile(fileId);
                if ( this._UserFile.FileStatus == UserFileStatus.已删除 || this._UserFile.FileStatus == UserFileStatus.停用 )
                {
                    throw new ErrorException("file.not.find");
                }
                service.RequestState.Set("file", this._UserFile);
            }
        }

        public void ReplyEvent ( IApiService service, IResponse response )
        {
            if ( this._UserFile == null || ( this._UserFile.Power != FilePower.共享 && this._UserFile.Power != FilePower.公共 ) )
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
                        Etag = this._UserFile.Etag,
                        LastModified = this._UserFile.SaveTime
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
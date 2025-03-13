using Base.FileCollect;
using Base.FileModel.UserFileDir;
using Base.FileRemoteModel;
using Base.FileService.Interface;
using WeDonekRpc.ApiGateway.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Log;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpService.FileUp;

namespace Base.FileGateway.FileCheck
{
    internal class UpFileCheck : IUpFileConfig
    {
        private static readonly ApiUpSet _Def = new ApiUpSet
        {
            IsGenerateMd5 = true
        };
        private readonly IUserFileDirCollect _UserFileDir;
        private readonly IDirService _DirService;
        public UpFileCheck ( IUserFileDirCollect fileDir, IDirService dir )
        {
            this._DirService = dir;
            this._UserFileDir = fileDir;
        }
        private UserFileDirDto _FileDir;
        private IDirState _Dir;

        public ApiUpSet UpSet { get; } = _Def;


        [ThreadStatic]
        internal static UserFileDirDto UserDir;

        [ThreadStatic]
        internal static IDirState SaveDir;

        public string ApplyTempSavePath ( IApiService service, UpFileParam param )
        {
            return this._Dir.CombinePath("tempUpFile", Guid.NewGuid().ToString("N") + Path.GetExtension(param.FileName));
        }

        public void CheckFileSize ( IApiService service, long fileSize )
        {
            if ( this._FileDir.AllowUpFileSize.HasValue && this._FileDir.AllowUpFileSize.Value < fileSize )
            {
                throw new ErrorException("file.up.size.limit");
            }
            this._Dir = this._DirService.GetDir(ref fileSize);
            SaveDir = this._Dir;
        }

        public void CheckUpFormat ( IApiService service, string fileName, int num )
        {
            if ( this._FileDir.AllowExtension.IsNull() )
            {
                return;
            }
            string ext = Path.GetExtension(fileName);
            if ( ext.IsNull() || !this._FileDir.AllowExtension.Contains(ext) )
            {
                throw new ErrorException("file.up.extension.error");
            }
        }

        public void InitFileUp ( IApiService service )
        {
            string dirKey = service.PathArgs.GetValueOrDefault("configkey");
            if ( dirKey.IsNull() )
            {
                throw new ErrorException("file.dir.key.null");
            }
            this._FileDir = this._UserFileDir.GetDir(dirKey);
            UserDir = this._FileDir;
            if ( this._FileDir.DirStatus == FileDirStatus.只读 )
            {
                throw new ErrorException("file.user.dir.readOnly");
            }
            else if ( this._FileDir.DirStatus == FileDirStatus.禁用 )
            {
                throw new ErrorException("file.user.dir.no.enable");
            }
            else if ( this._FileDir.IsAccredit )
            {
                service.CheckAccredit(this._FileDir.UpPower);
            }
        }


        public void UpError ( IApiService service, Exception e )
        {
            string userId = service.UserState == null ? string.Empty : service.UserState["UserId"].ToString();
            new ErrorLog(e, "文件上传错误!", "FileUp")
            {
                {"Uri", service.Request.Url },
                {"AccreditId",service.AccreditId },
                {"UserId", userId }
            }.Save();
        }
    }
}

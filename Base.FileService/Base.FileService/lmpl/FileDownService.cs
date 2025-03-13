using System.Net;
using Base.FileCollect;
using Base.FileModel.UserFileDir;
using Base.FileRemoteModel.Down.Model;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.lmpl
{
    internal class FileDownService : IFileDownService
    {
        private readonly IUserFileDirCollect _FileDir;
        private readonly IFileService _File;
        private readonly IDirService _PhysicalDir;

        public FileDownService ( IUserFileDirCollect fileDir,
            IFileService file,
            IDirService physicalDir )
        {
            this._FileDir = fileDir;
            this._File = file;
            this._PhysicalDir = physicalDir;
        }
        public bool AddClient ( string scheme, HttpClientParam config )
        {
            return HttpClientFactory.SetClient(scheme, a =>
             {
                 a.AutomaticDecompression = config.AutomaticDecompression;
                 a.AllowAutoRedirect = config.AllowAutoRedirect;
                 a.PreAuthenticate = config.PreAuthenticate;
                 a.UseDefaultCredentials = config.UseDefaultCredentials;
                 a.SslProtocols = config.SslProtocols;
                 a.CheckCertificateRevocationList = config.CheckCertificateRevocationList;
                 a.ClientCertificateOptions = config.ClientCertificateOptions;
                 if ( config.Cert != null )
                 {
                     CertInfo cert = config.Cert;
                     _ = a.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate(cert.CertFile, cert.Password, cert.KeyStorageFlags));
                 }
                 if ( config.Proxy != null )
                 {
                     HttpWebProxy proxy = config.Proxy;
                     if ( proxy.Credentials == null )
                     {
                         a.Proxy = new WebProxy(proxy.Address, proxy.BypassOnLocal, proxy.BypassList);
                     }
                     else
                     {
                         Credentials t = config.Proxy.Credentials;
                         a.Proxy = new WebProxy(proxy.Address, proxy.BypassOnLocal, proxy.BypassList, new NetworkCredential(t.UserName, t.Pwd, t.Domain));
                     }
                 }
             });
        }
        private HttpClient _Create ( string scheme )
        {
            if ( scheme.IsNotNull() )
            {
                return HttpClientFactory.Create(scheme);
            }
            return HttpClientFactory.Create();
        }
        public DownResult DownSmallFile ( Uri uri, DownFileParam obj )
        {
            UserFileDirDto dir = this._FileDir.GetDir(obj.DirKey);
            RequestSet req = obj.RequestSet.ToRequestSet();
            HttpResult res;
            using ( HttpClient client = this._Create(obj.Scheme) )
            {
                HttpRequestMessage message = obj.ToHttpRequestMessage(req, uri);
                res = client.SendRequest(message, req);
            }
            return this._SaveFile(new HttpMemoryFile(res, obj.FileName), new UpArg
            {
                LinkBizPk = obj.LinkBizPk,
                Tag = obj.Tag,
                UserDir = dir,
                UserId = obj.UserId.GetValueOrDefault(),
                UserType = obj.UserType,
            });
        }
        private DownResult _SaveFile ( IUpFile upFile, UpArg arg )
        {
            long fileSize = upFile.FileSize;
            arg.Dir = this._PhysicalDir.GetDir(ref fileSize);
            UserFile file = this._File.SaveFile(upFile, arg);
            return new DownResult
            {
                FileId = file.FileId,
                FileUri = file.FileUri,
                FileName = file.FileName
            };
        }
    }
}
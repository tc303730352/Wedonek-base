using System.Net;
using System.Security.Authentication;

namespace Base.FileRemoteModel.Down.Model
{
    public class HttpClientParam
    {
        /// <summary>
        /// 获取或设置一个值，该值指示处理程序是否应跟随重定向响应。
        /// </summary>
        public bool AllowAutoRedirect { get; set; } = false;

        /// <summary>
        /// 获取或设置一个值，该值指示处理程序是否随请求发送授权标头。
        /// </summary>
        public bool PreAuthenticate { get; set; } = false;
        /// <summary>
        /// 获取或设置 HttpClientHandler 对象管理的 HttpClient 对象所用的 TLS/SSL 协议。
        /// </summary>
        public SslProtocols SslProtocols { get; set; } = SslProtocols.Tls12;
        /// <summary>
        /// 获取或设置处理程序用于自动解压缩 HTTP 内容响应的解压缩方法类型。
        /// </summary>
        public DecompressionMethods AutomaticDecompression { get; set; } = DecompressionMethods.None;

        /// <summary>
        ///  获取或设置一个值，该值指示是否根据证书颁发机构吊销列表检查证书。
        /// </summary>
        public bool CheckCertificateRevocationList { get; set; } = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否从证书存储自动挑选证书，或者是否允许调用方通过特定的客户端证书。
        /// </summary>
        public ClientCertificateOption ClientCertificateOptions { get; set; } = ClientCertificateOption.Automatic;

        /// <summary>
        /// 证书信息
        /// </summary>
        public CertInfo Cert { get; set; }


        /// <summary>
        /// 获取或设置一个值，该值控制处理程序是否随请求一起发送默认凭据
        /// </summary>
        public bool UseDefaultCredentials { get; set; } = false;

        /// <summary>
        /// 代理对象
        /// </summary>
        public HttpWebProxy Proxy { get; set; }
    }
}

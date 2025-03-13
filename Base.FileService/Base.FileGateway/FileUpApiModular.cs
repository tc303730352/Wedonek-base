using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpService.Config;

namespace Base.FileGateway
{
    public class FileUpApiModular : BasicApiModular
    {
        public FileUpApiModular () : base("File_UpGateway")
        {
        }
        protected override void Load ( IHttpGatewayOption option, IModularConfig config )
        {
            this.Config.ApiRouteFormat = "/file/{controller}/{name}";
            option.AddFileDir(new FileDirConfig
            {
                DirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"File\json"),
                VirtualPath = "/file/json/",
                Extension = new string[]
                {
                    ".json"
                }
            });
        }

    }
}

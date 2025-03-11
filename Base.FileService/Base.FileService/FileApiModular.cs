using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpService.Config;

namespace Base.FileService
{
    internal class FileApiModular : BasicApiModular
    {
        public FileApiModular () : base("File_Modular")
        {
        }
        protected override void Init ()
        {
            this.Config.ApiRouteFormat = "/file/{controller}/{name}";
        }
        protected override void Load ( IHttpGatewayOption option, IModularConfig config )
        {
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

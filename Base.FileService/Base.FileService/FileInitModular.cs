using Base.FileService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Base.FileService
{
    internal class FileInitModular : IRpcInitModular
    {

        public void Init (IIocService ioc)
        {

        }

        public void InitEnd (IIocService ioc, IRpcService service)
        {
            service.StartUpComplating += this.Service_StartUpComplating;
        }

        private void Service_StartUpComplating ()
        {
            RpcClient.Ioc.Resolve<IDirService>().Init(RpcClient.ServerId);
        }

        public void Load (RpcInitOption option)
        {
        }
    }
}

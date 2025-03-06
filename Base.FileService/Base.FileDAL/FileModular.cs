using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;
using WeDonekRpc.SqlSugarDbTran;

namespace Base.FileDAL
{
    internal class FileModular : IRpcInitModular
    {
        public void Init (IIocService ioc)
        {
            IMapperCollect mapper = ioc.Resolve<IMapperCollect>();
            _ = mapper.Config.ConvertUsing<string[], string>((a) =>
            {
                if (a.IsNull())
                {
                    return null;
                }
                return string.Concat("|", a.Join('|'), "|");
            });
        }


        public void InitEnd (IIocService ioc, IRpcService service)
        {

        }


        public void Load (RpcInitOption option)
        {
            option.LoadModular<RpcTranModular>();
        }
    }
}

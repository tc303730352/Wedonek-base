using Basic.HrLocalEvent;
using WeDonekRpc.Client;
using WeDonekRpc.Modular;

namespace Basic.HrService
{
    public class HrService
    {
        public static void InitService ()
        {
            RpcClient.Start(( option ) =>
            {
                option.LoadModular<HrLocalEventModular>();
                option.LoadModular<ExtendModular>();
                option.Load("Basic.HrService");
            });
        }
    }
}

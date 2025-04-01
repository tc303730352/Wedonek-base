using Basic.FormLocalEvent;
using WeDonekRpc.Client;

namespace Basic.FormService
{
    public class FormService
    {
        public static void InitService ()
        {
            RpcClient.Start(( option ) =>
            {
                option.LoadModular<FormLocalEventModular>();
                option.Load("Basic.FormService");
            });
        }
    }
}

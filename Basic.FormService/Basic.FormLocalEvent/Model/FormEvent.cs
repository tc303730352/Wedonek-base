using Basic.FormModel.DB;
using WeDonekRpc.Client;

namespace Basic.FormLocalEvent.Model
{
    public class FormEvent : RpcLocalEvent
    {
        public FormEvent ( DBCustomForm form )
        {
            this.Form = form;
        }
        public DBCustomForm Form { get; }
    }
}
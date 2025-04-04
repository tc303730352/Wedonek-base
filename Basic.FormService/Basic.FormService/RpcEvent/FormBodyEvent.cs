using Basic.FormRemoteModel.Form;
using Basic.FormRemoteModel.Form.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.FormService.RpcEvent
{
    internal class FormBodyEvent : IRpcApiService
    {
        private readonly IFormBodyService _Service;

        public FormBodyEvent ( IFormBodyService service )
        {
            this._Service = service;
        }
        public FormBody GetFormBody ( GetFormBody obj )
        {
            return this._Service.Get(obj.Id);
        }
    }
}

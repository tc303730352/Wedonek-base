using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form;
using Basic.FormRemoteModel.Form.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.FormService.RpcEvent
{
    internal class FormEvent : IRpcApiService
    {
        private readonly ICustomFormService _Service;

        public FormEvent ( ICustomFormService service )
        {
            this._Service = service;
        }

        public long AddForm ( AddForm data )
        {
            return this._Service.Add(data.Datum);
        }

        public void DeleteForm ( DeleteForm obj )
        {
            this._Service.Delete(obj.Id);
        }

        public FormDto GetForm ( GetForm obj )
        {
            return this._Service.Get(obj.Id);
        }

        public PagingResult<FormDto> QueryForm ( QueryForm obj )
        {
            return this._Service.Query(obj.Query, obj);
        }

        public bool SetForm ( SetForm obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }

        public bool StopForm ( StopForm obj )
        {
            return this._Service.SetStatus(obj.Id, FormStatus.停用);
        }
    }
}

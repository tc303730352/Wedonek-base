using Basic.FormRemoteModel.Control;
using Basic.FormRemoteModel.Control.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.FormService.RpcEvent
{
    internal class ControlEvent : IRpcApiService
    {
        private readonly IControlService _Service;

        public ControlEvent ( IControlService service )
        {
            this._Service = service;
        }
        public ControlItem[] GetControlItems ( GetControlItems obj )
        {
            return this._Service.GetItems();
        }
        public bool SetControlStatus ( SetControlStatus obj )
        {
            return this._Service.SetStatus(obj.Id, obj.Status);
        }
        public long AddControl ( AddControl data )
        {
            return this._Service.Add(data.Datum);
        }

        public void DeleteControl ( DeleteControl obj )
        {
            this._Service.Delete(obj.Id);
        }

        public ControlDetailed GetControl ( GetControl obj )
        {
            return this._Service.Get(obj.Id);
        }

        public PagingResult<ControlDto> QueryControl ( QueryControl obj )
        {
            return this._Service.Query(obj.Query, obj);
        }

        public bool SetControl ( SetControl set )
        {
            return this._Service.Set(set.Id, set.Datum);
        }
    }
}

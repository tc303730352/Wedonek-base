using Basic.HrRemoteModel.Power;
using Basic.HrRemoteModel.Power.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class PowerEvent : IRpcApiService
    {
        private readonly IPowerService _Service;

        public PowerEvent ( IPowerService service )
        {
            this._Service = service;
        }
        public bool SetPowerSort ( SetPowerSort obj )
        {
            return this._Service.SetSort(obj.Id, obj.Sort);
        }
        public PowerDataTree[] GetPowerTrees ( GetPowerTrees obj )
        {
            return this._Service.GetTrees(obj.Query);
        }
        public long AddPower ( AddPower add )
        {
            return this._Service.Add(add.Datum);
        }
        public PagingResult<PowerBase> QueryPower ( QueryPower obj )
        {
            return this._Service.Query(obj.QueryParam, obj.ToBasicPage());
        }
        public void DeletePower ( DeletePower obj )
        {
            this._Service.Delete(obj.Id);
        }
        public PowerTree[] GetPowerTree ( GetPowerTree obj )
        {
            return this._Service.GetPowerTree(obj.SubSystemId, obj.Param);
        }
        public PowerSubSystem[] GetPowerTreeBySystem ( GetPowerTreeBySystem obj )
        {
            return this._Service.GetTrees(obj.Param, obj.CompanyId);
        }
        public bool SetPower ( SetPower obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
        public PowerData GetPower ( GetPower obj )
        {
            return this._Service.Get(obj.Id);
        }
        public bool SetPowerIsEnable ( SetPowerIsEnable obj )
        {
            return this._Service.SetIsEnable(obj.Id, obj.IsEnable);
        }
    }
}

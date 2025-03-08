using Basic.HrRemoteModel.Prower;
using Basic.HrRemoteModel.Prower.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class ProwerEvent : IRpcApiService
    {
        private readonly IProwerService _Service;

        public ProwerEvent ( IProwerService service )
        {
            this._Service = service;
        }
        public bool SetProwerSort ( SetProwerSort obj )
        {
            return this._Service.SetSort(obj.Id, obj.Sort);
        }
        public ProwerDataTree[] GetProwerTrees ( GetProwerTrees obj )
        {
            return this._Service.GetTrees(obj.Query);
        }
        public long AddPrower ( AddPrower add )
        {
            return this._Service.Add(add.Datum);
        }
        public PagingResult<ProwerBase> QueryPrower ( QueryPrower obj )
        {
            return this._Service.Query(obj.QueryParam, obj.ToBasicPage());
        }
        public ProwerTree[] GetProwerTree ( GetProwerTree obj )
        {
            return this._Service.GetProwerTree(obj.SubSystemId, obj.IsEnable);
        }
        public ProwerSubSystem[] GetProwerTreeBySystem ( GetProwerTreeBySystem obj )
        {
            return this._Service.GetTrees(obj.Param);
        }
        public bool SetPrower ( SetPrower obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
        public ProwerData GetPrower ( GetPrower obj )
        {
            return this._Service.Get(obj.Id);
        }
    }
}

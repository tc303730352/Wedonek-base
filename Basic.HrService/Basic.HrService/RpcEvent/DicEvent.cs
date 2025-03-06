using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic;
using Basic.HrRemoteModel.Dic.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class DicEvent : IRpcApiService
    {
        private readonly IDicService _Service;

        public DicEvent (IDicService service)
        {
            this._Service = service;
        }
        public bool EnableDic (EnableDic obj)
        {
            return this._Service.SetStatus(obj.Id, DicStatus.启用);
        }
        public bool StopDic (StopDic obj)
        {
            return this._Service.SetStatus(obj.Id, DicStatus.停用);
        }
        public void DeleteDic (DeleteDic obj)
        {
            this._Service.Delete(obj.Id);
        }
        public bool SetDic (SetDic obj)
        {
            return this._Service.Update(obj.Id, obj.Datum);
        }
        public long AddDic (AddDic add)
        {
            return this._Service.Add(add.Datum);
        }
        public DicDto GetDic (GetDic obj)
        {
            return this._Service.Get(obj.Id);
        }
        public PagingResult<DicDatum> QueryDic (QueryDic obj)
        {
            return this._Service.Query(obj.Query, obj.ToBasicPage());
        }
    }
}

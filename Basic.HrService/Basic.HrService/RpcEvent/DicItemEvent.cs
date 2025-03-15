using Basic.HrRemoteModel.DicItem;
using Basic.HrRemoteModel.DicItem.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class DicItemEvent : IRpcApiService
    {
        private readonly IDicItemService _Service;

        public DicItemEvent ( IDicItemService service )
        {
            this._Service = service;
        }
        public string[] GetDicTextList ( GetDicTextList obj )
        {
            return this._Service.GetTextList(obj.DicId, obj.Values);
        }
        public void StopDicItem ( StopDicItem obj )
        {
            this._Service.Stop(obj.Id);
        }
        public DicItemDto GetDicItem ( GetDicItem obj )
        {
            return this._Service.Get(obj.Id);
        }
        public long AddDicItem ( AddDicItem add )
        {
            return this._Service.Add(add.Datum);
        }
        public void EnableDicItem ( EnableDicItem obj )
        {
            this._Service.Enable(obj.Id);
        }
        public void DeleteDicItem ( DeleteDicItem obj )
        {
            this._Service.Delete(obj.Id);
        }

        public Dictionary<long, DicItem[]> GetItemDic ( GetItemDic obj )
        {
            return this._Service.GetItemDic(obj.DicId);
        }
        public void MoveDicItem ( MoveDicItem obj )
        {
            this._Service.Move(obj.Id, obj.ToId);
        }
        public DicItem[] GetDicItems ( GetDicItems obj )
        {
            return this._Service.GetItems(obj.DicId);
        }

        public DicItemDto[] GetAllDicItems ( GetAllDicItems obj )
        {
            return this._Service.Gets(obj.Query);
        }

        public bool SetDicItem ( SetDicItem set )
        {
            return this._Service.Set(set.Id, set.ItemSet);
        }
    }
}

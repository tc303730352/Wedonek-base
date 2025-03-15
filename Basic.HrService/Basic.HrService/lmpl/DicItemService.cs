using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.DicItem.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;

namespace Basic.HrService.lmpl
{
    internal class DicItemService : IDicItemService
    {
        private readonly IDicItemCollect _DictItem;

        public DicItemService ( IDicItemCollect dictItem )
        {
            this._DictItem = dictItem;
        }

        public long Add ( DicItemAdd item )
        {
            return this._DictItem.Add(item);
        }

        public void Delete ( long id )
        {
            DBDicItem item = this._DictItem.Get(id);
            this._DictItem.Delete(item);
        }
        public void Move ( long id, long toId )
        {
            DBDicItem item = this._DictItem.Get(id);
            DBDicItem to = this._DictItem.Get(toId);
            this._DictItem.Move(item, to);
        }
        public DicItem[] GetItems ( long dictId )
        {
            return this._DictItem.GetItems(dictId);
        }
        public Dictionary<long, DicItem[]> GetItemDic ( long[] dicId )
        {
            return this._DictItem.GetItems(dicId).GroupBy(a => a.DicId).ToDictionary(a => a.Key, a => a.Select(c => new DicItem
            {
                DicText = c.DicText,
                DicValue = c.DicValue
            }).ToArray());
        }
        public DicItemDto[] Gets ( DicItemQuery query )
        {
            return this._DictItem.Gets<DicItemDto>(query);
        }
        public DicItemDto Get ( long id )
        {
            DBDicItem item = this._DictItem.Get(id);
            return item.ConvertMap<DBDicItem, DicItemDto>();
        }
        public bool Set ( long id, DicItemSet set )
        {
            DBDicItem item = this._DictItem.Get(id);
            return this._DictItem.Set(item, set);
        }

        public void Enable ( long id )
        {
            DBDicItem item = this._DictItem.Get(id);
            this._DictItem.Enable(item);
        }
        public void Stop ( long id )
        {
            DBDicItem item = this._DictItem.Get(id);
            this._DictItem.Stop(item);
        }

        public string[] GetTextList ( long dicId, string[] values )
        {
            return this._DictItem.GetTextList(dicId, values);
        }
    }
}

using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.DicItem;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.DicItem.Model;
using SqlSugar;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class DicItemCollect : IDicItemCollect
    {
        private readonly IDicItemDAL _DictItem;

        public DicItemCollect (IDicItemDAL dictItem)
        {
            this._DictItem = dictItem;
        }
        public DicItem[] GetItems (long dicId)
        {
            return this._DictItem.GetItems(dicId);
        }
        public DicItemBase[] GetItems (long[] dicId)
        {
            return this._DictItem.GetItems(dicId);
        }
        public void Delete (DBDicItem item)
        {
            if (item.DicStatus == DicItemStatus.启用)
            {
                throw new ErrorException("hr.dic.item.not.allow.update");
            }
            this._DictItem.Delete(item.Id);
        }
        public bool Set (DBDicItem item, DicItemSet set)
        {
            if (item.DicStatus == DicItemStatus.启用)
            {
                throw new ErrorException("hr.dic.item.not.allow.update");
            }
            else if (item.DicText != set.DicText && this._DictItem.IsExists(c => c.DicId == item.DicId && c.DicText == set.DicText))
            {
                throw new ErrorException("hr.dic.item.text.repeat");
            }
            return this._DictItem.Update(item, set);
        }
        public long Add (DicItemAdd item)
        {
            if (item.DicValue.IsNotNull() && this._DictItem.IsExists(c => c.DicId == item.DicId && c.DicValue == item.DicValue))
            {
                throw new ErrorException("hr.dic.item.value.repeat");
            }
            if (this._DictItem.IsExists(c => c.DicId == item.DicId && c.DicText == item.DicText))
            {
                throw new ErrorException("hr.dic.item.text.repeat");
            }
            DBDicItem add = item.ConvertMap<DicItemAdd, DBDicItem>();
            add.Sort = this._DictItem.GetSort(item.DicId) + 1;
            if (add.DicValue.IsNull())
            {
                string max = this._DictItem.Get(a => a.DicId == item.DicId && a.IsAuto, a => SqlFunc.AggregateMax(a.DicValue));
                if (max.IsNull())
                {
                    add.DicValue = "001";
                }
                else
                {
                    add.DicValue = ( int.Parse(max) + 1 ).ToString().PadLeft(3, '0');
                }
            }
            this._DictItem.Add(add);
            return add.Id;
        }
        public string[] GetTextList (long dictId, string[] values)
        {
            return this._DictItem.GetTextList(dictId, values);
        }

        public Dictionary<string, string> GetTexts (long dictId, string[] values)
        {
            return this._DictItem.GetTexts(dictId, values);
        }

        public DBDicItem Get (long id)
        {
            return this._DictItem.Get(id);
        }

        public void Enable (DBDicItem item)
        {
            if (item.DicStatus == DicItemStatus.启用)
            {
                return;
            }
            this._DictItem.SetStatus(item, DicItemStatus.启用);
        }

        public void Stop (DBDicItem item)
        {
            if (item.DicStatus == DicItemStatus.停用)
            {
                return;
            }
            this._DictItem.SetStatus(item, DicItemStatus.停用);
        }
        public Dictionary<long, int> GetItemNum (long[] dicId)
        {
            if (dicId.IsNull())
            {
                return null;
            }
            return this._DictItem.GetItemNum(dicId);
        }

        public void Clear (long dicId)
        {
            long[] ids = this._DictItem.Gets(a => a.DicId == dicId, a => a.Id);
            if (ids.IsNull())
            {
                return;
            }
            this._DictItem.Delete(ids);
        }

        public Result[] Gets<Result> (DicItemQuery query) where Result : class, new()
        {
            return this._DictItem.Gets<Result>(query);
        }

        public void Move (DBDicItem item, DBDicItem toItem)
        {
            this._DictItem.Move(item, toItem);
        }
    }
}

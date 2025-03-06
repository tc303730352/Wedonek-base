using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;
namespace Basic.HrCollect.Impl
{
    internal class TreeDicItemCollect : ITreeDicItemCollect
    {
        private readonly ITreeDicItemDAL _DicItemDAL;
        public TreeDicItemCollect (ITreeDicItemDAL dicItemDAL)
        {
            this._DicItemDAL = dicItemDAL;
        }
        public bool Stop (DBTreeDicItem item)
        {
            if (item.DicStatus == DicItemStatus.停用)
            {
                return false;
            }
            long[] subId = this._DicItemDAL.GetSubIds(item.DicId, item.LevelCode, DicItemStatus.启用);
            this._DicItemDAL.Stop(subId.Add(item.Id));
            return true;
        }
        public bool Enable (DBTreeDicItem item)
        {
            if (item.DicStatus == DicItemStatus.启用)
            {
                return false;
            }
            if (item.ParentId != 0)
            {
                DicItemStatus prt = this._DicItemDAL.Get(item.ParentId, a => a.DicStatus);
                if (prt != DicItemStatus.启用)
                {
                    throw new ErrorException("hr.dic.tree.parent.no.enable");
                }
            }
            this._DicItemDAL.Enable(item);
            return true;
        }
        public TreeItem[] GetEnableItems (long dictId)
        {
            return this._DicItemDAL.GetEnableItems(dictId);
        }
        public void Move (DBTreeDicItem source, DBTreeDicItem to)
        {
            if (source.DicId != to.DicId)
            {
                throw new ErrorException("hr.dic.tree.disaccord");
            }
            else if (source.ParentId != to.ParentId)
            {
                throw new ErrorException("hr.dic.tree.disaccord");
            }
            this._DicItemDAL.SetSort(source, to);
        }
        public long Add (TreeDicItemAdd add)
        {
            DicItemStatus[] status = new DicItemStatus[]
            {
                DicItemStatus.起草,
                DicItemStatus.启用
            };
            if (this._DicItemDAL.IsExists(c => c.DicId == add.DicId && c.ParentId == add.ParentId && c.DicText == add.DicText && status.Contains(c.DicStatus)))
            {
                throw new ErrorException("hr.dic.tree.text.repeat");
            }
            else if (add.DicValue.IsNotNull() && this._DicItemDAL.IsExists(c => c.DicId == add.DicId && c.DicValue == add.DicValue))
            {
                throw new ErrorException("hr.dic.tree.value.repeat");
            }
            DBTreeDicItem item = add.ConvertMap<TreeDicItemAdd, DBTreeDicItem>();
            item.DicStatus = DicItemStatus.起草;
            item.Sort = this._DicItemDAL.GetSort(add.DicId, add.ParentId) + 1;
            if (add.ParentId != 0)
            {
                var prt = this._DicItemDAL.Get(add.ParentId, a => new
                {
                    a.DicId,
                    a.DicValue,
                    a.LevelCode,
                    a.DicLvl
                });
                if (prt.DicId != add.DicId)
                {
                    throw new ErrorException("hr.dic.tree.disaccord");
                }
                item.DicLvl = prt.DicLvl + 1;
                item.LevelCode = prt.LevelCode + add.ParentId + "|";
                if (item.DicValue.IsNull())
                {
                    string maxValue = this._DicItemDAL.Get(c => c.DicId == add.DicId && c.ParentId == add.ParentId && c.IsAutoGenerate, c => SqlSugar.SqlFunc.AggregateMax(c.DicValue));
                    item.DicValue = prt.DicValue + ( int.Parse(maxValue.Remove(0, prt.DicValue.Length)) + 1 ).ToString().PadLeft(2, '0');
                    item.IsAutoGenerate = true;
                }
            }
            else
            {
                item.LevelCode = "|0|";
                item.DicLvl = 1;
                if (item.DicValue.IsNull())
                {
                    string maxValue = this._DicItemDAL.Get(c => c.DicId == add.DicId && c.ParentId == 0 && c.IsAutoGenerate, c => SqlSugar.SqlFunc.AggregateMax(c.DicValue));
                    item.DicValue = ( int.Parse(maxValue) + 1 ).ToString().PadLeft(2, '0');
                    item.IsAutoGenerate = true;
                }
            }
            this._DicItemDAL.Add(item);
            return item.Id;
        }
        public bool Set (DBTreeDicItem item, TreeDicItemSet set)
        {
            if (item.DicText == set.DicText)
            {
                return false;
            }
            else if (this._DicItemDAL.IsExists(c => c.DicId == item.DicId && c.ParentId == item.ParentId && c.DicText == set.DicText))
            {
                throw new ErrorException("hr.dic.tree.text.repeat");
            }
            return this._DicItemDAL.Update(item, set);
        }
        public DBTreeDicItem Get (long id)
        {
            return this._DicItemDAL.Get(id);
        }
        public DBTreeDicItem Get (long dicId, string dictValue)
        {
            return this._DicItemDAL.Get(dicId, dictValue);
        }
        public void Delete (DBTreeDicItem item)
        {
            if (item.DicStatus != DicItemStatus.起草)
            {
                throw new ErrorException("hr.dic.tree.not.allow.delete");
            }
            long[] subId = this._DicItemDAL.GetSubIds(item.DicId, item.LevelCode);
            this._DicItemDAL.Delete(subId.Add(item.Id));
        }
        public string GetText (long dictId, string value)
        {
            return this._DicItemDAL.GetText(dictId, value);
        }

        public Dictionary<string, string> GetTexts (long dictId, string[] values)
        {
            return this._DicItemDAL.GetTexts(dictId, values);
        }

        public DBTreeDicItem[] Query (TreeItemQuery query, IBasicPage paging, out int count)
        {
            paging.InitOrderBy("Id", true);
            return this._DicItemDAL.Query(query, paging, out count);
        }
        public Result[] Gets<Result> (TreeItemQuery query) where Result : class, new()
        {
            return this._DicItemDAL.Gets<Result>(query);
        }

        public Dictionary<long, int> GetItemNum (long[] dicId)
        {
            if (dicId.IsNull())
            {
                return null;
            }
            return this._DicItemDAL.GetItemNum(dicId);
        }

        public void Clear (long dicId)
        {
            long[] ids = this._DicItemDAL.Gets(a => a.DicId == dicId, a => a.Id);
            if (ids.IsNull())
            {
                return;
            }
            this._DicItemDAL.Delete(ids);
        }

        public string GetItemValue (long id)
        {
            return this._DicItemDAL.Get(id, a => a.DicValue);
        }
    }
}

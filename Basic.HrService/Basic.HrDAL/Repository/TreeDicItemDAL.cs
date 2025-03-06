using Basic.HrModel.DB;
using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.TreeDic.Model;
using SqlSugar;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class TreeDicItemDAL : BasicDAL<DBTreeDicItem, long>, ITreeDicItemDAL
    {
        public TreeDicItemDAL (IRepository<DBTreeDicItem> basicDAL) : base(basicDAL)
        {
        }
        public void Stop (long[] ids)
        {
            if (!this._BasicDAL.Update(a => a.DicStatus == DicItemStatus.停用, a => ids.Contains(a.Id)))
            {
                throw new ErrorException("hr.dic.tree.stop.fail");
            }
        }
        public void Enable (DBTreeDicItem item)
        {
            if (!this._BasicDAL.Update(a => new DBTreeDicItem
            {
                DicStatus = DicItemStatus.启用
            }, a => a.Id == item.Id))
            {
                throw new ErrorException("hr.dic.tree.enable.fail");
            }
        }
        public string GetText (long dictId, string value)
        {
            return this._BasicDAL.Get(a => a.DicId == dictId && a.DicValue == value, a => a.DicText);
        }
        public TreeItem[] GetEnableItems (long dictId)
        {
            return this._BasicDAL.Gets<TreeItem>(a => a.DicId == dictId && a.DicStatus == DicItemStatus.启用, "Sort");
        }
        public DBTreeDicItem Get (long dictId, string value)
        {
            return this._BasicDAL.Get(a => a.DicId == dictId && a.DicValue == value);
        }
        public Dictionary<string, string> GetTexts (long dictId, string[] values)
        {
            return this._BasicDAL.Gets(a => a.DicId == dictId && values.Contains(a.DicValue), a => new
            {
                a.DicValue,
                a.DicText
            }).ToDictionary(a => a.DicValue, a => a.DicText);
        }
        public void Delete (long[] ids)
        {
            if (!this._BasicDAL.Delete(a => ids.Contains(a.Id)))
            {
                throw new Exception("hr.tree.item.delete.fail");
            }
        }
        public long[] GetSubIds (long dictId, string levelCode)
        {
            return this._BasicDAL.Gets(a => a.DicId == dictId && a.LevelCode.StartsWith(levelCode), a => a.Id);
        }
        public long[] GetSubIds (long dictId, string levelCode, DicItemStatus status)
        {
            return this._BasicDAL.Gets(a => a.DicId == dictId && a.LevelCode.StartsWith(levelCode) && a.DicStatus == status, a => a.Id);
        }
        public void Add (DBTreeDicItem add)
        {
            add.Id = IdentityHelper.CreateId();
            add.DicStatus = DicItemStatus.起草;
            this._BasicDAL.Insert(add);
        }

        public int GetSort (long dicId, long parentId)
        {
            return this._BasicDAL.Max(c => c.DicId == dicId && c.ParentId == parentId, a => a.Sort);
        }

        public TreeItemSet[] GetSub (long dicId, string levelCode)
        {
            return this._BasicDAL.Gets<TreeItemSet>(a => a.DicId == dicId && a.LevelCode.StartsWith(levelCode));
        }

        public void Set (TreeItemSet[] items)
        {
            if (!this._BasicDAL.Update<TreeItemSet>(items))
            {
                throw new Exception("hr.tree.item.updte.fail");
            }
        }

        public DBTreeDicItem[] Query (TreeItemQuery query, IBasicPage paging, out int count)
        {
            return this._BasicDAL.Query(query.ToWhere(), paging, out count);
        }
        public Result[] Gets<Result> (TreeItemQuery query) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(query.ToWhere());
        }
        public void SetSort (DBTreeDicItem source, DBTreeDicItem to)
        {
            ISqlQueue<DBTreeDicItem> queue = this._BasicDAL.BeginQueue();
            queue.UpdateOneColumn(a => a.Sort == to.Sort, c => c.Id == source.Id);
            queue.UpdateOneColumn(a => a.Sort == source.Sort, c => c.Id == to.Id);
            _ = queue.Submit();
        }
        public Dictionary<long, int> GetItemNum (long[] dicId)
        {
            return this._BasicDAL.GroupBy(a => dicId.Contains(a.DicId), a => a.DicId, a => new
            {
                a.DicId,
                num = SqlFunc.AggregateCount(a.DicId)
            }).ToDictionary(a => a.DicId, a => a.num);
        }
    }
}

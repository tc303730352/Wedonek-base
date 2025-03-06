using Basic.HrModel.DB;
using Basic.HrModel.DicItem;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.DicItem.Model;
using SqlSugar;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class DicItemDAL : BasicDAL<DBDicItem, long>, IDicItemDAL
    {
        public DicItemDAL (IRepository<DBDicItem> basicDAL) : base(basicDAL)
        {
        }

        public void Add (DBDicItem item)
        {
            item.Id = IdentityHelper.CreateId();
            item.DicStatus = DicItemStatus.起草;
            this._BasicDAL.Insert(item);
        }

        public Result[] Gets<Result> (DicItemQuery query) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(query.ToWhere());
        }
        public DicItemBase[] GetItems (long[] dicId)
        {
            return this._BasicDAL.Gets<DicItemBase>(a => dicId.Contains(a.DicId) && a.DicStatus == DicItemStatus.启用, "Sort");
        }
        public DicItem[] GetItems (long dictId)
        {
            return this._BasicDAL.Gets<DicItem>(a => a.DicId == dictId && a.DicStatus == DicItemStatus.启用, "Sort");
        }

        public int GetSort (long dicId)
        {
            return this._BasicDAL.Max(c => c.DicId == dicId, a => a.Sort);
        }

        public string[] GetTextList (long dictId, string[] values)
        {
            return this._BasicDAL.Gets(a => a.DicId == dictId && values.Contains(a.DicValue), a => a.DicText);
        }

        public Dictionary<string, string> GetTexts (long dictId, string[] values)
        {
            return this._BasicDAL.Gets(a => a.DicId == dictId && values.Contains(a.DicValue), a => new
            {
                a.DicValue,
                a.DicText
            }).ToDictionary(a => a.DicValue, a => a.DicText);
        }

        public void SetStatus (DBDicItem item, DicItemStatus status)
        {
            if (!this._BasicDAL.Update(a => a.DicStatus == status, a => a.Id == item.Id))
            {
                throw new ErrorException("hr.dic.item.status.set.fail");
            }
        }
        public Dictionary<long, int> GetItemNum (long[] dicId)
        {
            return this._BasicDAL.GroupBy(a => dicId.Contains(a.DicId), a => a.DicId, a => new
            {
                a.DicId,
                num = SqlFunc.AggregateCount(a.DicId)
            }).ToDictionary(a => a.DicId, a => a.num);
        }

        public void Move (DBDicItem item, DBDicItem to)
        {
            ISqlQueue<DBDicItem> queue = this._BasicDAL.BeginQueue();
            queue.UpdateOneColumn(a => a.Sort == to.Sort, c => c.Id == item.Id);
            queue.UpdateOneColumn(a => a.Sort == item.Sort, c => c.Id == to.Id);
            _ = queue.Submit();
        }
    }
}

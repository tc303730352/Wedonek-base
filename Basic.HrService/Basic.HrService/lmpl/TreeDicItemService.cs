using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel.TreeDic.Model;
using Basic.HrService.Interface;
using Basic.HrService.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class TreeDicItemService : ITreeDicItemService
    {
        private readonly ITreeDicItemCollect _DicItem;

        public TreeDicItemService (ITreeDicItemCollect dicItem)
        {
            this._DicItem = dicItem;
        }

        public long Add (TreeDicItemAdd add)
        {
            return this._DicItem.Add(add);
        }
        public void Delete (long id)
        {
            DBTreeDicItem item = this._DicItem.Get(id);
            this._DicItem.Delete(item);
        }

        public TreeDicItemDto Get (long id)
        {
            DBTreeDicItem item = this._DicItem.Get(id);
            TreeDicItemDto dto = item.ConvertMap<DBTreeDicItem, TreeDicItemDto>();
            if (item.ParentId != 0)
            {
                dto.ParentValue = this._DicItem.GetItemValue(item.ParentId);
            }
            return dto;
        }
        public bool Set (long id, TreeDicItemSet set)
        {
            DBTreeDicItem item = this._DicItem.Get(id);
            return this._DicItem.Set(item, set);
        }
        public TreeItemBase[] GetTrees (long dicId)
        {
            TreeItem[] items = this._DicItem.GetEnableItems(dicId);
            return items.Convert(c => c.ParentId == 0, c => new TreeItemBase
            {
                Id = c.Id,
                DicText = c.DicText,
                DicValue = c.DicValue,
                Children = items.ToTree(c)
            });
        }
        public TreeFullItem[] GetFullTree (TreeItemQuery query)
        {
            TreeItemTemp[] items = this._DicItem.Gets<TreeItemTemp>(query);
            if (items.IsNull())
            {
                return null;
            }
            items = items.OrderBy(a => a.Sort).ToArray();
            int level = items.Min(c => c.DicLvl);
            return items.Convert(c => c.DicLvl == level, c => new TreeFullItem
            {
                Id = c.Id,
                DicStatus = c.DicStatus,
                IsEnd = c.IsEnd,
                DicText = c.DicText,
                DicValue = c.DicValue,
                Sort = c.Sort,
                Children = items.ToTree(c)
            });
        }
        public void Move (long fromId, long toId)
        {
            DBTreeDicItem from = this._DicItem.Get(fromId);
            DBTreeDicItem to = this._DicItem.Get(toId);
            this._DicItem.Move(from, to);
        }
        public PagingResult<TreeDicItemDto> Query (TreeItemQuery query, IBasicPage paging)
        {
            DBTreeDicItem[] items = this._DicItem.Query(query, paging, out int count);
            return new PagingResultTo<DBTreeDicItem, TreeDicItemDto>(count, items);
        }

        public bool Enable (long id)
        {
            DBTreeDicItem item = this._DicItem.Get(id);
            return this._DicItem.Enable(item);
        }
        public bool Stop (long id)
        {
            DBTreeDicItem item = this._DicItem.Get(id);
            return this._DicItem.Stop(item);
        }
    }
}

using Basic.HrModel.DB;
using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface ITreeDicItemCollect
    {
        bool Stop (DBTreeDicItem item);
        bool Enable (DBTreeDicItem item);
        long Add (TreeDicItemAdd add);
        void Delete (DBTreeDicItem item);
        DBTreeDicItem Get (long id);
        DBTreeDicItem Get (long dicId, string dictValue);
        TreeItem[] GetEnableItems (long dictId);
        string GetText (long dictId, string value);
        string GetItemValue (long id);
        Dictionary<string, string> GetTexts (long dictId, string[] values);
        void Move (DBTreeDicItem source, DBTreeDicItem to);
        DBTreeDicItem[] Query (TreeItemQuery query, IBasicPage paging, out int count);
        bool Set (DBTreeDicItem item, TreeDicItemSet set);
        Dictionary<long, int> GetItemNum (long[] dicId);
        void Clear (long dicId);
        Result[] Gets<Result> (TreeItemQuery query) where Result : class, new();
        string[] GetItemNames ( long dicId, string[] values );
    }
}
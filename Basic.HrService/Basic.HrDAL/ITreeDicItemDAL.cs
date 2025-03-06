using Basic.HrModel.DB;
using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface ITreeDicItemDAL : IBasicDAL<DBTreeDicItem, long>
    {
        Result[] Gets<Result> (TreeItemQuery query) where Result : class, new();
        void Stop (long[] ids);
        TreeItem[] GetEnableItems (long dictId);
        DBTreeDicItem Get (long dictId, string value);
        string GetText (long dictId, string value);
        Dictionary<string, string> GetTexts (long dictId, string[] values);
        void Delete (long[] ids);
        long[] GetSubIds (long dictId, string levelCode);
        long[] GetSubIds (long dictId, string levelCode, DicItemStatus status);
        void Add (DBTreeDicItem add);
        int GetSort (long dicId, long parentId);
        TreeItemSet[] GetSub (long dicId, string levelCode);
        void Set (TreeItemSet[] items);
        DBTreeDicItem[] Query (TreeItemQuery query, IBasicPage paging, out int count);
        void Enable (DBTreeDicItem item);
        Dictionary<long, int> GetItemNum (long[] dicId);
        void SetSort (DBTreeDicItem source, DBTreeDicItem to);
    }
}
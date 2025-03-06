using Basic.HrModel.DB;
using Basic.HrModel.DicItem;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.DicItem.Model;

namespace Basic.HrDAL
{
    public interface IDicItemDAL : IBasicDAL<DBDicItem, long>
    {
        Result[] Gets<Result> (DicItemQuery query) where Result : class, new();
        void Add (DBDicItem item);
        DicItem[] GetItems (long dictId);
        DicItemBase[] GetItems (long[] dictId);
        int GetSort (long dicId);
        string[] GetTextList (long dictId, string[] values);
        Dictionary<string, string> GetTexts (long dictId, string[] values);
        void SetStatus (DBDicItem item, DicItemStatus status);
        Dictionary<long, int> GetItemNum (long[] dicId);
        void Move (DBDicItem item, DBDicItem toItem);
    }
}
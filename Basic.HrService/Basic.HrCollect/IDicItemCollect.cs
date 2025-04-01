using Basic.HrModel.DB;
using Basic.HrModel.DicItem;
using Basic.HrRemoteModel.DicItem.Model;

namespace Basic.HrCollect
{
    public interface IDicItemCollect
    {
        DicItem[] GetItems ( long dicId );
        DicItemBase[] GetItems ( long[] dicId );
        bool Set ( DBDicItem item, DicItemSet set );
        void Delete ( DBDicItem item );
        long Add ( DicItemAdd item );
        string[] GetTextList ( long dicId, string[] values );
        Dictionary<string, string> GetTexts ( long dicId, string[] values );
        DBDicItem Get ( long id );
        void Enable ( DBDicItem item );
        void Stop ( DBDicItem item );
        Dictionary<long, int> GetItemNum ( long[] dicId );
        void Clear ( long dicId );
        Result[] Gets<Result> ( long[] dicId ) where Result : class, new();
        Result[] Gets<Result> ( DicItemQuery query ) where Result : class, new();
        void Move ( DBDicItem item, DBDicItem toItem );
    }
}
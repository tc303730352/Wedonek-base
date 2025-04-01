using Basic.HrRemoteModel.DicItem.Model;

namespace Basic.HrService.Interface
{
    public interface IDicItemService
    {
        long Add ( DicItemAdd item );
        void Delete ( long id );
        void Enable ( long id );
        void Move ( long id, long toId );
        Dictionary<long, DicItem[]> GetItemDic ( long[] dicId );
        DicItem[] GetItems ( long dictId );
        DicItemDto[] Gets ( DicItemQuery query );
        bool Set ( long id, DicItemSet set );
        void Stop ( long id );
        DicItemDto Get ( long id );
        string[] GetTextList ( long dicId, string[] values );
        DicItemName[] GetNames ( long[] dicId );
    }
}
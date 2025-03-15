using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.DicItem;
using Basic.HrRemoteModel.DicItem.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class DicItemService : IDicItemService
    {
        public long AddDicItem ( DicItemAdd datum )
        {
            return new AddDicItem
            {
                Datum = datum,
            }.Send();
        }

        public void DeleteDicItem ( long id )
        {
            new DeleteDicItem
            {
                Id = id,
            }.Send();
        }

        public void EnableDicItem ( long id )
        {
            new EnableDicItem
            {
                Id = id,
            }.Send();
        }
        public Dictionary<string, string> GetItemName ( long dicId )
        {
            return new GetDicItems
            {
                DicId = dicId,
            }.Send().ToDictionary(a => a.DicValue, a => a.DicText);
        }
        public DicItem[] GetDicItems ( long dicId )
        {
            return new GetDicItems
            {
                DicId = dicId,
            }.Send();
        }

        public DicItemDto[] Gets ( DicItemQuery query )
        {
            return new GetAllDicItems
            {
                Query = query
            }.Send();
        }

        public bool SetDicItem ( long id, DicItemSet itemSet )
        {
            return new SetDicItem
            {
                Id = id,
                ItemSet = itemSet,
            }.Send();
        }

        public void StopDicItem ( long id )
        {
            new StopDicItem
            {
                Id = id,
            }.Send();
        }

        public void Move ( long id, long toId )
        {
            new MoveDicItem
            {
                Id = id,
                ToId = toId,
            }.Send();
        }

        public DicItemDto GetDicItem ( long id )
        {
            return new GetDicItem
            {
                Id = id
            }.Send();
        }

        public string[] GetDicTextList ( long dicId, string[] values )
        {
            return new GetDicTextList
            {
                DicId = dicId,
                Values = values
            }.Send();
        }
    }
}

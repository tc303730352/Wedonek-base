using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.TreeDic;
using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class TreeDicService : ITreeDicService
    {
        public long AddTreeDicItem ( TreeDicItemAdd datum )
        {
            return new AddTreeDicItem
            {
                Datum = datum,
            }.Send();
        }

        public void DeleteTreeDicItem ( long id )
        {
            new DeleteTreeDicItem
            {
                Id = id,
            }.Send();
        }
        public string[] GetTreeNames ( long dicId, string[] vals )
        {
            return new GetTreeNames
            {
                DicId = dicId,
                Values = vals
            }.Send();
        }

        public bool EnableTreeDicItem ( long id )
        {
            return new EnableTreeDicItem
            {
                Id = id,
            }.Send();
        }
        public TreeFullItem[] GetTree ( TreeItemQuery query )
        {
            return new GetFullDicTree
            {
                Query = query
            }.Send();
        }
        public TreeItemBase[] GetDicTree ( long dicId )
        {
            return new GetDicTree
            {
                DicId = dicId,
            }.Send();
        }

        public TreeDicItemDto GetTreeDicItem ( long id )
        {
            return new GetTreeDicItem
            {
                Id = id,
            }.Send();
        }

        public void MoveTreeDicItem ( long fromId, long toId )
        {
            new MoveTreeDicItem
            {
                FromId = fromId,
                ToId = toId,
            }.Send();
        }

        public TreeDicItemDto[] QueryTreeDicItem ( TreeItemQuery query, IBasicPage paging, out int count )
        {
            return new QueryTreeDicItem
            {
                Query = query,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out count);
        }

        public bool SetTreeDicItem ( long id, TreeDicItemSet datum )
        {
            return new SetTreeDicItem
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public bool StopTreeDicItem ( long id )
        {
            return new StopTreeDicItem
            {
                Id = id,
            }.Send();
        }

    }
}

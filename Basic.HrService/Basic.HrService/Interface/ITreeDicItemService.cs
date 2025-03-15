using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface ITreeDicItemService
    {
        TreeFullItem[] GetFullTree (TreeItemQuery query);
        long Add (TreeDicItemAdd add);
        void Delete (long id);
        bool Enable (long id);
        TreeDicItemDto Get (long id);
        TreeItemBase[] GetTrees (long dicId);
        void Move (long fromId, long toId);
        PagingResult<TreeDicItemDto> Query (TreeItemQuery query, IBasicPage paging);
        bool Set (long id, TreeDicItemSet set);
        bool Stop (long id);
        string[] GetTreeNames ( long dicId, string[] values );
    }
}
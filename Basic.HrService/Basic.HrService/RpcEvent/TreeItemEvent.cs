using Basic.HrRemoteModel.TreeDic;
using Basic.HrRemoteModel.TreeDic.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class TreeItemEvent : IRpcApiService
    {
        private readonly ITreeDicItemService _Service;

        public TreeItemEvent (ITreeDicItemService service)
        {
            this._Service = service;
        }
        public bool EnableTreeDicItem (EnableTreeDicItem obj)
        {
            return this._Service.Enable(obj.Id);
        }
        public bool StopTreeDicItem (StopTreeDicItem obj)
        {
            return this._Service.Stop(obj.Id);
        }
        public long AddTreeDicItem (AddTreeDicItem add)
        {
            return this._Service.Add(add.Datum);
        }

        public void DeleteTreeDicItem (DeleteTreeDicItem obj)
        {
            this._Service.Delete(obj.Id);
        }
        public TreeFullItem[] GetFullDicTree (GetFullDicTree obj)
        {
            return this._Service.GetFullTree(obj.Query);
        }
        public TreeDicItemDto GetTreeDicItem (GetTreeDicItem obj)
        {
            return this._Service.Get(obj.Id);
        }

        public TreeItemBase[] GetDicTree (GetDicTree obj)
        {
            return this._Service.GetTrees(obj.DicId);
        }

        public void MoveTreeDicItem (MoveTreeDicItem obj)
        {
            this._Service.Move(obj.FromId, obj.ToId);
        }

        public PagingResult<TreeDicItemDto> QueryTreeDicItem (QueryTreeDicItem obj)
        {
            return this._Service.Query(obj.Query, obj.ToBasicPage());
        }

        public bool SetTreeDicItem (SetTreeDicItem set)
        {
            return this._Service.Set(set.Id, set.Datum);
        }
    }
}

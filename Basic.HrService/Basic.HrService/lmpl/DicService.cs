using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class DicService : IDicService
    {
        private readonly IDicCollect _Dic;
        private readonly ITreeDicItemCollect _TreeItem;
        private readonly IDicItemCollect _DicItem;

        public DicService (IDicCollect dic,
            ITreeDicItemCollect treeItem,
            IDicItemCollect dicItem)
        {
            this._Dic = dic;
            this._TreeItem = treeItem;
            this._DicItem = dicItem;
        }

        public long Add (DicAdd add)
        {
            return this._Dic.Add(add);
        }

        public void Delete (long id)
        {
            DBDicList sour = this._Dic.Get(id);
            this._Dic.Delete(sour);
        }

        public DicDto Get (long id)
        {
            DBDicList sour = this._Dic.Get(id);
            return sour.ConvertMap<DBDicList, DicDto>();
        }

        public PagingResult<DicDatum> Query (DicQuery query, IBasicPage paging)
        {
            DBDicList[] sour = this._Dic.Query(query, paging, out int count);
            if (sour.IsNull())
            {
                return new PagingResult<DicDatum>(count);
            }
            DicDatum[] list = sour.ConvertMap<DBDicList, DicDatum>();
            Dictionary<long, int> tree = this._TreeItem.GetItemNum(list.Convert(a => a.IsTreeDic, a => a.Id));
            Dictionary<long, int> dicItem = this._DicItem.GetItemNum(list.Convert(a => !a.IsTreeDic, a => a.Id));
            list.ForEach(c =>
            {
                c.ItemNum = c.IsTreeDic ? tree.GetValueOrDefault(c.Id) : dicItem.GetValueOrDefault(c.Id);
            });
            return new PagingResult<DicDatum>(list, count);
        }

        public bool SetStatus (long id, DicStatus status)
        {
            DBDicList sour = this._Dic.Get(id);
            return this._Dic.SetStatus(sour, status);
        }

        public bool Update (long id, DicSet set)
        {
            DBDicList sour = this._Dic.Get(id);
            return this._Dic.Update(sour, set);
        }
    }
}

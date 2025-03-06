using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class DicCollect : IDicCollect
    {
        private readonly IDicListDAL _Dic;

        public DicCollect (IDicListDAL dic)
        {
            this._Dic = dic;
        }

        public long Add (DicAdd add)
        {
            if (this._Dic.IsExists(c => c.DicName == add.DicName))
            {
                throw new ErrorException("hr.dic.name.repeat");
            }
            DBDicList db = add.ConvertMap<DicAdd, DBDicList>();
            this._Dic.Add(db);
            return db.Id;
        }

        public void Delete (DBDicList db)
        {
            if (db.Status != DicStatus.起草)
            {
                throw new ErrorException("hr.dic.not.can.delete");
            }
            else if (db.IsSysDic)
            {
                throw new ErrorException("hr.sys.dic.not.can.delete");
            }
            this._Dic.Delete(db);
        }
        public DBDicList Get (long id)
        {
            return this._Dic.Get(id);
        }

        public DBDicList[] Query (DicQuery query, IBasicPage paging, out int count)
        {
            return this._Dic.Query(query, paging, out count);
        }

        public bool SetStatus (DBDicList sour, DicStatus status)
        {
            if (sour.Status == status)
            {
                return false;
            }
            this._Dic.SetStatus(sour, status);
            return true;
        }

        public bool Update (DBDicList source, DicSet set)
        {
            if (source.IsSysDic)
            {
                throw new ErrorException("hr.sys.dic.not.can.update");
            }
            else if (source.DicName != set.DicName && this._Dic.IsExists(c => c.DicName == set.DicName))
            {
                throw new ErrorException("hr.dic.name.repeat");
            }
            return this._Dic.Update(source, set);
        }
    }
}

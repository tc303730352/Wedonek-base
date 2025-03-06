using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class DicListDAL : BasicDAL<DBDicList, long>, IDicListDAL
    {
        public DicListDAL (IRepository<DBDicList> basicDAL) : base(basicDAL)
        {
        }
        public void Add (DBDicList db)
        {
            db.Id = IdentityHelper.CreateId();
            db.Status = DicStatus.起草;
            db.IsSysDic = false;
            this._BasicDAL.Insert(db);
        }
        public DBDicList[] Query (DicQuery query, IBasicPage paging, out int count)
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query(query.ToWhere(), paging, out count);
        }
        public void Delete (DBDicList db)
        {
            ISqlQueue<DBDicList> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => a.Id == db.Id);
            if (db.IsTreeDic)
            {
                queue.Delete<DBTreeDicItem>(a => a.DicId == db.Id);
            }
            else
            {
                queue.Delete<DBDicItem>(a => a.DicId == db.Id);
            }
            if (queue.Submit() <= 0)
            {
                throw new ErrorException("hr.dic.delete.fail");
            }
        }

        public void SetStatus (DBDicList sour, DicStatus status)
        {
            if (!this._BasicDAL.Update(a => a.Status == status, a => a.Id == sour.Id))
            {
                throw new ErrorException("hr.dic.status.set.fail");
            }
        }
    }
}

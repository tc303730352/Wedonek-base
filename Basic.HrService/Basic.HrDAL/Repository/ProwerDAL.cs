using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class ProwerDAL : BasicDAL<DBProwerList, long>, IProwerDAL
    {
        public ProwerDAL ( IRepository<DBProwerList> basicDAL ) : base(basicDAL)
        {
        }
        public DBProwerList[] GetEnables ( long subSystemId )
        {
            return this._BasicDAL.Gets(a => a.SubSystemId == subSystemId && a.IsEnable);
        }
        public DBProwerList[] GetEnables ()
        {
            return this._BasicDAL.Gets(a => a.IsEnable);
        }
        public T[] Gets<T> ( ProwerQuery query ) where T : class, new()
        {
            return this._BasicDAL.Gets<T>(query.ToWhere());
        }
        public Result[] Query<Result> ( ProwerQuery query, IBasicPage paging, out int count ) where Result : class, new()
        {
            paging.InitOrderBy("Id", false);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
        public void Add ( DBProwerList add )
        {
            add.Id = IdentityHelper.CreateId();
            add.IsEnable = false;
            add.AddTime = DateTime.Now;
            this._BasicDAL.Insert(add);
        }
        public void Enable ( DBProwerList source )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == true, a => a.Id == source.Id) )
            {
                throw new ErrorException("hr.prower.enable.fail");
            }
        }
        public long[] GetAllSubId ( string levelCode, bool? isEnable )
        {
            if ( isEnable.HasValue )
            {
                return this._BasicDAL.Gets(a => a.LevelCode.StartsWith(levelCode) && a.IsEnable == isEnable.Value, a => a.Id);
            }
            return this._BasicDAL.Gets(a => a.LevelCode.StartsWith(levelCode), a => a.Id);
        }
        public void Stop ( long[] ids )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == false, a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.prower.stop.fail");
            }
        }
        public int GetSort ( long subSysId, long parentId )
        {
            return this._BasicDAL.Max(a => a.SubSystemId == subSysId && a.ParentId == parentId, a => a.Sort);
        }

        public void SetRelation ( ProwerRelationSet[] sets )
        {
            if ( !this._BasicDAL.Update<ProwerRelationSet>(sets) )
            {
                throw new ErrorException("hr.prower.delete.fail");
            }
        }

        public string GetHomeUri ( long subSysId )
        {
            return this._BasicDAL.Get(a => a.SubSystemId == subSysId && a.ProwerType == HrRemoteModel.ProwerType.menu && a.IsEnable, a => a.RouteName, "LevelNum,Sort");
        }
        public string GetHomeUri ( long subSysId, long[] prowerId )
        {
            return this._BasicDAL.Get(a => a.SubSystemId == subSysId && prowerId.Contains(a.Id) && a.IsEnable, a => a.RouteName, "LevelNum,Sort");
        }

        public T[] Gets<T> ( long[] subSysId, ProwerGetParam param ) where T : class, new()
        {
            return this._BasicDAL.Gets<T>(param.ToWhere(subSysId));
        }

        public void SetSort ( DBProwerList db, int sort )
        {
            if ( !this._BasicDAL.Update(a => a.Sort == sort, a => a.Id == db.Id) )
            {
                throw new ErrorException("hr.prower.sort.set.fail");
            }
        }
    }
}

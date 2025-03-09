using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class PowerDAL : BasicDAL<DBPowerList, long>, IPowerDAL
    {
        public PowerDAL ( IRepository<DBPowerList> basicDAL ) : base(basicDAL)
        {
        }
        public DBPowerList[] GetEnables ( long subSystemId )
        {
            return this._BasicDAL.Gets(a => a.SubSystemId == subSystemId && a.IsEnable);
        }
        public DBPowerList[] GetEnables ()
        {
            return this._BasicDAL.Gets(a => a.IsEnable);
        }
        public T[] Gets<T> ( PowerQuery query ) where T : class, new()
        {
            return this._BasicDAL.Gets<T>(query.ToWhere());
        }
        public Result[] Query<Result> ( PowerQuery query, IBasicPage paging, out int count ) where Result : class, new()
        {
            paging.InitOrderBy("Id", false);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
        public void Add ( DBPowerList add )
        {
            add.Id = IdentityHelper.CreateId();
            add.IsEnable = false;
            add.AddTime = DateTime.Now;
            this._BasicDAL.Insert(add);
        }
        public void Enable ( DBPowerList source )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == true, a => a.Id == source.Id) )
            {
                throw new ErrorException("hr.power.enable.fail");
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
                throw new ErrorException("hr.power.stop.fail");
            }
        }
        public int GetSort ( long subSysId, long parentId )
        {
            return this._BasicDAL.Max(a => a.SubSystemId == subSysId && a.ParentId == parentId, a => a.Sort);
        }

        public void SetRelation ( PowerRelationSet[] sets )
        {
            if ( !this._BasicDAL.Update<PowerRelationSet>(sets) )
            {
                throw new ErrorException("hr.power.delete.fail");
            }
        }

        public string GetHomeUri ( long subSysId )
        {
            return this._BasicDAL.Get(a => a.SubSystemId == subSysId && a.PowerType == HrRemoteModel.PowerType.menu && a.IsEnable, a => a.RouteName, "LevelNum,Sort");
        }
        public string GetHomeUri ( long subSysId, long[] powerId )
        {
            return this._BasicDAL.Get(a => a.SubSystemId == subSysId && powerId.Contains(a.Id) && a.IsEnable, a => a.RouteName, "LevelNum,Sort");
        }

        public T[] Gets<T> ( long[] subSysId, PowerGetParam param ) where T : class, new()
        {
            return this._BasicDAL.Gets<T>(param.ToWhere(subSysId));
        }

        public void SetSort ( DBPowerList db, int sort )
        {
            if ( !this._BasicDAL.Update(a => a.Sort == sort, a => a.Id == db.Id) )
            {
                throw new ErrorException("hr.power.sort.set.fail");
            }
        }
    }
}

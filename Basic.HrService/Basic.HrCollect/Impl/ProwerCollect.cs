using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class ProwerCollect : IProwerCollect
    {
        private readonly IProwerDAL _Prower;

        public ProwerCollect ( IProwerDAL prower )
        {
            this._Prower = prower;
        }
        public Result[] Query<Result> ( ProwerQuery query, IBasicPage paging, out int count ) where Result : class, new()
        {
            return this._Prower.Query<Result>(query, paging, out count);
        }
        public ProwerRouteDto[] GetRoutes ( long subSysId )
        {
            return this._Prower.Gets<ProwerRouteDto>(a => a.SubSystemId == subSysId && a.ProwerType == ProwerType.menu && a.IsEnable).OrderBy(a => a.Sort).ToArray();
        }
        public ProwerRouteDto[] GetEnables ()
        {
            return this._Prower.Gets<ProwerRouteDto>(a => a.IsEnable).OrderBy(a => a.Sort).ToArray();
        }
        public ProwerBasic[] Gets ( long[] ids )
        {
            return this._Prower.Gets<ProwerBasic>(ids);
        }
        public ProwerBasic[] GetFulls ( long[] ids )
        {
            var dtos = this._Prower.Gets(ids, a => new
            {
                a.Id,
                a.ParentId,
                a.SubSystemId,
                a.ProwerType
            });
            List<long> pid = dtos.Where(a => a.ParentId != 0 && !ids.Contains(a.ParentId)).Select(a => a.ParentId).Distinct().ToList();
            if ( pid.Count == 0 )
            {
                return dtos.ConvertAll(a => new ProwerBasic
                {
                    Id = a.Id,
                    SubSystemId = a.SubSystemId,
                    ProwerType = a.ProwerType
                });
            }
            List<ProwerBasic> list = dtos.ConvertAllToList(a => new ProwerBasic
            {
                Id = a.Id,
                SubSystemId = a.SubSystemId,
                ProwerType = a.ProwerType
            });
            do
            {
                dtos = this._Prower.Gets(a => pid.Contains(a.Id), a => new
                {
                    a.Id,
                    a.ParentId,
                    a.SubSystemId,
                    a.ProwerType
                });
                pid.Clear();
                dtos.ForEach(a =>
                {
                    list.Add(new ProwerBasic
                    {
                        Id = a.Id,
                        SubSystemId = a.SubSystemId,
                        ProwerType = a.ProwerType
                    });
                    if ( a.ParentId != 0 )
                    {
                        pid.Add(a.ParentId);
                    }
                });
            } while ( pid.Count > 0 );
            return list.ToArray();
        }
        public ProwerDto[] GetDtos ( long[] subSystemId, ProwerGetParam getParam )
        {
            return this._Prower.Gets<ProwerDto>(subSystemId, getParam).OrderBy(a => a.Sort).ToArray();
        }
        public ProwerDto[] GetDtos ( long subSystemId, bool? isEnable )
        {
            if ( isEnable.HasValue )
            {
                return this._Prower.Gets<ProwerDto>(a => a.SubSystemId == subSystemId && a.IsEnable == isEnable.Value).OrderBy(a => a.Sort).ToArray();
            }
            return this._Prower.Gets<ProwerDto>(a => a.SubSystemId == subSystemId).OrderBy(a => a.Sort).ToArray();
        }
        public bool Set ( DBProwerList sour, ProwerSet set )
        {
            if ( sour.IsEnable )
            {
                throw new ErrorException("hr.prower.not.can.update");
            }
            else if ( set.Name != sour.Name && this._Prower.IsExists(c => c.SubSystemId == sour.SubSystemId
            && c.ParentId == sour.ParentId
            && c.Name == set.Name) )
            {
                throw new ErrorException("hr.prower.name.repeat");
            }
            return this._Prower.Update(sour, set);
        }

        public long Add ( ProwerAdd add )
        {
            if ( this._Prower.IsExists(c => c.SubSystemId == add.SubSystemId
            && c.ParentId == add.ParentId
            && c.Name == add.Name) )
            {
                throw new ErrorException("hr.prower.name.repeat");
            }
            DBProwerList db = add.ConvertMap<ProwerAdd, DBProwerList>();
            db.Sort = this._Prower.GetSort(add.SubSystemId, add.ParentId) + 1;
            if ( add.ParentId != 0 )
            {
                string level = this._Prower.Get(add.ParentId, a => a.LevelCode);
                db.LevelCode = level + add.ParentId + "|";
            }
            this._Prower.Add(db);
            return db.Id;
        }

        public DBProwerList Get ( long id )
        {
            return this._Prower.Get(id);
        }

        public ProwerDto[] GetDtos ( long subSystemId )
        {
            return this._Prower.Gets<ProwerDto>(a => a.SubSystemId == subSystemId);
        }

        public string GetHomeUri ( long subSystemId )
        {
            return this._Prower.GetHomeUri(subSystemId);
        }
    }
}

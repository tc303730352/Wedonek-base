using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class PowerCollect : IPowerCollect
    {
        private readonly IPowerDAL _Power;

        public PowerCollect ( IPowerDAL power )
        {
            this._Power = power;
        }
        public T[] Gets<T> ( PowerQuery query ) where T : class, new()
        {
            return this._Power.Gets<T>(query);
        }
        public Result[] Query<Result> ( PowerQuery query, IBasicPage paging, out int count ) where Result : class, new()
        {
            return this._Power.Query<Result>(query, paging, out count);
        }
        public PowerRouteDto[] GetRoutes ( long subSysId )
        {
            return this._Power.Gets<PowerRouteDto>(a => a.SubSystemId == subSysId && a.PowerType == PowerType.menu && a.IsEnable).OrderBy(a => a.Sort).ToArray();
        }
        public PowerRouteDto[] GetEnables ( long[] ids )
        {
            PowerRouteDto[] list;
            if ( ids.IsNull() )
            {
                list = this._Power.Gets<PowerRouteDto>(a => a.IsEnable);
            }
            else
            {
                list = this._Power.Gets<PowerRouteDto>(a => ids.Contains(a.Id) && a.IsEnable);
            }
            List<long> prtId = new List<long>();
            list.ForEach(c =>
            {
                if ( c.LevelCode != string.Empty )
                {
                    c.LevelCode.SplitWriteLong('|', prtId);
                }
            });
            long[] pids = prtId.Distinct().ToArray();
            PowerRouteDto[] menus = this._Power.Gets<PowerRouteDto>(a => pids.Contains(a.Id) && a.PowerType == PowerType.dir);
            return list.Add(menus).OrderBy(a => a.Sort).ToArray();
        }
        public PowerBasic[] Gets ( long[] ids )
        {
            return this._Power.Gets<PowerBasic>(ids);
        }
        public PowerBasic[] GetFulls ( long[] ids )
        {
            var dtos = this._Power.Gets(ids, a => new
            {
                a.Id,
                a.ParentId,
                a.SubSystemId,
                a.PowerType
            });
            List<long> pid = dtos.Where(a => a.ParentId != 0 && !ids.Contains(a.ParentId)).Select(a => a.ParentId).Distinct().ToList();
            if ( pid.Count == 0 )
            {
                return dtos.ConvertAll(a => new PowerBasic
                {
                    Id = a.Id,
                    SubSystemId = a.SubSystemId,
                    PowerType = a.PowerType
                });
            }
            List<PowerBasic> list = dtos.ConvertAllToList(a => new PowerBasic
            {
                Id = a.Id,
                SubSystemId = a.SubSystemId,
                PowerType = a.PowerType
            });
            do
            {
                dtos = this._Power.Gets(a => pid.Contains(a.Id), a => new
                {
                    a.Id,
                    a.ParentId,
                    a.SubSystemId,
                    a.PowerType
                });
                pid.Clear();
                dtos.ForEach(a =>
                {
                    list.Add(new PowerBasic
                    {
                        Id = a.Id,
                        SubSystemId = a.SubSystemId,
                        PowerType = a.PowerType
                    });
                    if ( a.ParentId != 0 )
                    {
                        pid.Add(a.ParentId);
                    }
                });
            } while ( pid.Count > 0 );
            return list.ToArray();
        }
        public PowerDto[] GetDtos ( long[] subSystemId, PowerGetParam param )
        {
            return this._Power.Gets<PowerDto>(subSystemId, param).OrderBy(a => a.Sort).ToArray();
        }
        public long[] Filters ( long[] ids, PowerType powerType )
        {
            return this._Power.Gets(a => ids.Contains(a.Id) && a.PowerType == powerType, a => a.Id);
        }
        public PowerDto[] GetDtos ( long subSystemId, PowerGetParam param )
        {
            return this._Power.Gets<PowerDto>(subSystemId, param).OrderBy(a => a.Sort).ToArray();
        }
        public bool Set ( DBPowerList sour, PowerSet set )
        {
            if ( sour.IsEnable )
            {
                throw new ErrorException("hr.power.not.can.update");
            }
            else if ( set.Name != sour.Name && this._Power.IsExists(c => c.SubSystemId == sour.SubSystemId
            && c.ParentId == sour.ParentId
            && c.Name == set.Name) )
            {
                throw new ErrorException("hr.power.name.repeat");
            }
            return this._Power.Update(sour, set);
        }

        public long Add ( PowerAdd add )
        {
            if ( this._Power.IsExists(c => c.SubSystemId == add.SubSystemId
            && c.ParentId == add.ParentId
            && c.Name == add.Name) )
            {
                throw new ErrorException("hr.power.name.repeat");
            }
            DBPowerList db = add.ConvertMap<PowerAdd, DBPowerList>();
            db.Sort = this._Power.GetSort(add.SubSystemId, add.ParentId) + 1;
            if ( add.ParentId != 0 )
            {
                var prt = this._Power.Get(add.ParentId, a => new
                {
                    a.LevelCode,
                    a.LevelNum
                });
                if ( prt.LevelNum == 1 )
                {
                    db.LevelCode = "|" + add.ParentId + "|";
                }
                else
                {
                    db.LevelCode = prt.LevelCode + add.ParentId + "|";
                }
                db.LevelNum = prt.LevelNum + 1;
            }
            else
            {
                db.LevelNum = 1;
            }
            this._Power.Add(db);
            return db.Id;
        }

        public DBPowerList Get ( long id )
        {
            return this._Power.Get(id);
        }

        public PowerDto[] GetDtos ( long subSystemId )
        {
            return this._Power.Gets<PowerDto>(a => a.SubSystemId == subSystemId);
        }

        public string GetHomeUri ( long subSystemId )
        {
            return this._Power.GetHomeUri(subSystemId);
        }

        public bool SetSort ( DBPowerList db, int sort )
        {
            if ( db.Sort == sort )
            {
                return false;
            }
            this._Power.SetSort(db, sort);
            return true;
        }

        public void Delete ( DBPowerList db )
        {
            if ( db.IsEnable )
            {
                throw new ErrorException("hr.power.not.can.enable");
            }
            this._Power.Delete(db.Id);
        }

        public bool SetIsEnable ( DBPowerList db, bool isEnable )
        {
            if ( db.IsEnable == isEnable )
            {
                return false;
            }
            this._Power.SetIsEnable(db.Id, isEnable);
            return true;
        }
    }
}

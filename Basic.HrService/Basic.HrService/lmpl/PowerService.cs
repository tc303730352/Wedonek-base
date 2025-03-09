using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.SubSystem;
using Basic.HrRemoteModel.Power.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class PowerService : IPowerService
    {
        private readonly ISubSystemCollect _SubSystem;
        private readonly IPowerCollect _Power;

        public PowerService ( ISubSystemCollect subSystem,
            IPowerCollect power )
        {
            this._SubSystem = subSystem;
            this._Power = power;
        }
        public long Add ( PowerAdd add )
        {
            return this._Power.Add(add);
        }
        public bool SetSort ( long id, int sort )
        {
            DBPowerList db = this._Power.Get(id);
            return this._Power.SetSort(db, sort);
        }
        public PowerData Get ( long id )
        {
            DBPowerList db = this._Power.Get(id);
            return db.ConvertMap<DBPowerList, PowerData>();
        }
        public PowerTree[] GetPowerTree ( long subSysId, PowerGetParam param )
        {
            PowerDto[] dtos = this._Power.GetDtos(subSysId, param);
            return dtos.Convert(c => c.ParentId == 0, c =>
            {
                PowerTree tree = new PowerTree
                {
                    Id = c.Id,
                    Icon = c.Icon,
                    Description = c.Description,
                    Name = c.Name,
                    PowerType = c.PowerType
                };
                this._InitChildren(tree, dtos);
                return tree;
            });
        }
        public PowerSubSystem[] GetTrees ( PowerGetParam param )
        {
            SubSystemDto[] subs = this._SubSystem.Gets(param.IsEnable);
            if ( subs.IsNull() )
            {
                return null;
            }
            PowerDto[] dtos = this._Power.GetDtos(subs.ConvertAll(a => a.Id), param);
            return subs.ConvertAll(a => new PowerSubSystem
            {
                SubSysId = a.Id,
                SubSysName = a.Name,
                Powers = dtos.Convert(c => c.SubSystemId == a.Id && c.ParentId == 0, c =>
                {
                    PowerTree tree = new PowerTree
                    {
                        Id = c.Id,
                        Icon = c.Icon,
                        Description = c.Description,
                        Name = c.Name,
                        PowerType = c.PowerType
                    };
                    this._InitChildren(tree, dtos);
                    return tree;
                })
            });
        }

        public PagingResult<PowerBase> Query ( PowerQuery query, IBasicPage paging )
        {
            PowerBase[] list = this._Power.Query<PowerBase>(query, paging, out int count);
            return new PagingResult<PowerBase>(list, count);
        }
        public PowerDataTree[] GetTrees ( PowerQuery query )
        {
            PowerBaseDto[] list = this._Power.Gets<PowerBaseDto>(query);
            if ( list.IsNull() )
            {
                return Array.Empty<PowerDataTree>();
            }
            int lvl = list.Min(c => c.LevelNum);
            return list.Convert(c => c.LevelNum == lvl, c =>
            {
                return new PowerDataTree
                {
                    Id = c.Id,
                    Icon = c.Icon,
                    Description = c.Description,
                    Name = c.Name,
                    PowerType = c.PowerType,
                    Sort = c.Sort,
                    IsEnable = c.IsEnable,
                    RouteName = c.RouteName,
                    Children = this._GetChildren(c, list)
                };
            });
        }
        public PowerDataTree[] _GetChildren ( PowerBaseDto prt, PowerBaseDto[] list )
        {
            return list.Convert(c => c.ParentId == prt.Id, c =>
            {
                return new PowerDataTree
                {
                    Id = c.Id,
                    Icon = c.Icon,
                    Description = c.Description,
                    Name = c.Name,
                    PowerType = c.PowerType,
                    Sort = c.Sort,
                    IsEnable = c.IsEnable,
                    RouteName = c.RouteName,
                    Children = this._GetChildren(c, list)
                };
            });
        }
        public bool Set ( long id, PowerSet datum )
        {
            DBPowerList db = this._Power.Get(id);
            return this._Power.Set(db, datum);
        }

        private void _InitChildren ( PowerTree tree, PowerDto[] powers )
        {
            PowerDto[] list = powers.FindAll(c => c.ParentId == tree.Id);
            if ( !list.IsNull() )
            {
                tree.Children = list.ConvertAll(c =>
                {
                    PowerTree tree = new PowerTree
                    {
                        Id = c.Id,
                        Icon = c.Icon,
                        Description = c.Description,
                        Name = c.Name,
                        PowerType = c.PowerType
                    };
                    this._InitChildren(tree, powers);
                    return tree;
                });
            }
        }
    }
}

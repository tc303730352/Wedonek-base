using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrModel.SubSystem;
using Basic.HrRemoteModel.Prower.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class ProwerService : IProwerService
    {
        private readonly ISubSystemCollect _SubSystem;
        private readonly IProwerCollect _Prower;

        public ProwerService (ISubSystemCollect subSystem,
            IProwerCollect prower)
        {
            this._SubSystem = subSystem;
            this._Prower = prower;
        }
        public long Add (ProwerAdd add)
        {
            return this._Prower.Add(add);
        }

        public ProwerData Get (long id)
        {
            DBProwerList db = this._Prower.Get(id);
            return db.ConvertMap<DBProwerList, ProwerData>();
        }
        public ProwerTree[] GetProwerTree (long subSysId, bool? isEnable)
        {
            ProwerDto[] dtos = this._Prower.GetDtos(subSysId, isEnable);
            return dtos.Convert(c => c.ParentId == 0, c =>
            {
                ProwerTree tree = new ProwerTree
                {
                    Id = c.Id,
                    Icon = c.Icon,
                    Description = c.Description,
                    Name = c.Name,
                    ProwerType = c.ProwerType
                };
                this._InitChildren(tree, dtos);
                return tree;
            });
        }
        public ProwerSubSystem[] GetTrees ()
        {
            SubSystemDto[] subs = this._SubSystem.GetEnables();
            if (subs.IsNull())
            {
                return null;
            }
            ProwerDto[] dtos = this._Prower.GetDtos(subs.ConvertAll(a => a.Id));
            return subs.ConvertAll(a => new ProwerSubSystem
            {
                SubSysId = a.Id,
                SubSysName = a.Name,
                Prowers = dtos.Convert(c => c.SubSystemId == a.Id && c.ParentId == 0, c =>
                {
                    ProwerTree tree = new ProwerTree
                    {
                        Id = c.Id,
                        Icon = c.Icon,
                        Description = c.Description,
                        Name = c.Name,
                        ProwerType = c.ProwerType
                    };
                    this._InitChildren(tree, dtos);
                    return tree;
                })
            });
        }

        public PagingResult<ProwerBase> Query (ProwerQuery query, IBasicPage paging)
        {
            ProwerBase[] list = this._Prower.Query<ProwerBase>(query, paging, out int count);
            return new PagingResult<ProwerBase>(list, count);
        }

        public bool Set (long id, ProwerSet datum)
        {
            DBProwerList db = this._Prower.Get(id);
            return this._Prower.Set(db, datum);
        }

        private void _InitChildren (ProwerTree tree, ProwerDto[] prowers)
        {
            tree.Children = prowers.Convert(c => c.ParentId == tree.Id, c =>
            {
                ProwerTree tree = new ProwerTree
                {
                    Id = c.Id,
                    Icon = c.Icon,
                    Description = c.Description,
                    Name = c.Name,
                    ProwerType = c.ProwerType
                };
                this._InitChildren(tree, prowers);
                return tree;
            });
        }
    }
}

using Basic.FormCollect;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.TableGroup.Model;
using Basic.FormService.Interface;

namespace Basic.FormService.lmpl
{
    internal class TableGroupService : ITableGroupService
    {
        private readonly ITableGroupCollect _Group;

        public TableGroupService ( ITableGroupCollect group )
        {
            this._Group = group;
        }

        public long Add ( TableGroupAdd data )
        {
            return this._Group.Add(data);
        }


        public void Delete ( long id )
        {
            DBTableGroup source = this._Group.Get(id);
            this._Group.Delete(source);
        }

        public TableGroupDto Get ( long id )
        {
            return this._Group.Get<TableGroupDto>(id);
        }

        public bool Set ( long id, TableGroupSet set )
        {
            DBTableGroup source = this._Group.Get(id);
            return this._Group.Set(source, set);
        }

        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._Group.SetSort(sort);
        }
    }
}

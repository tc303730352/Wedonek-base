﻿using Basic.FormGatewaryModular.Interface;
using Basic.FormGatewaryModular.Model;
using Basic.FormRemoteModel.TableGroup;
using Basic.FormRemoteModel.TableGroup.Model;

namespace Basic.FormGatewaryModular.Services
{
    internal class TableGroupService : ITableGroupService
    {
        public long Add ( TableGroupAdd datum )
        {
            return new AddTableGroup
            {
                Datum = datum,
            }.Send();
        }

        public void Delete ( long id )
        {
            new DeleteTableGroup
            {
                Id = id,
            }.Send();
        }

        public TableGroupDto Get ( long id )
        {
            return new GetTableGroup
            {
                Id = id,
            }.Send();
        }

        public bool Set ( long id, TableGroupSet datum )
        {
            return new SetTableGroup
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public void SetSort ( SetSort[] sort )
        {
            new SetTableGroupSort
            {
                Sort = sort.Select(x => new KeyValuePair<long, int>(x.Id, x.Sort)).ToArray()
            }.Send();
        }

    }
}

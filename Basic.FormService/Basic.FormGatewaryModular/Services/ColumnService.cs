using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Column;
using Basic.FormRemoteModel.Column.Model;

namespace Basic.FormGatewaryModular.Services
{
    internal class ColumnService : IColumnService
    {
        public long Add ( TableColumnAdd datum )
        {
            return new AddColumn
            {
                Datum = datum,
            }.Send();
        }

        public void Delete ( long id )
        {
            new DeleteColumn
            {
                Id = id,
            }.Send();
        }

        public TableColumnDto Get ( long id )
        {
            return new GetColumn
            {
                Id = id,
            }.Send();
        }

        public bool Set ( long id, TableColumnSet datum )
        {
            return new SetColumn
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public bool SetGroupId ( long id, long groupId )
        {
            return new SetColumnGroupId
            {
                Id = id,
                GroupId = groupId,
            }.Send();
        }

        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            new SetColumnSort
            {
                Sort = sort,
            }.Send();
        }

        public bool SetSpan ( long id, int colSpan )
        {
            return new SetColumnSpan
            {
                Id = id,
                ColSpan = colSpan,
            }.Send();
        }

    }
}

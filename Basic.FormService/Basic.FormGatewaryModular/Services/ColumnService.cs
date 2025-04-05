using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Column;
using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.HttpApiGateway.Model;

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
        public void SaveSpan ( LongNullParam<int>[] span )
        {
            new SaveColumnSpan
            {
                ColSpan = span.Select(a => new KeyValuePair<long, int>(a.Id, a.Value)).ToArray(),
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

        public void SetSort ( LongNullParam<int>[] sort )
        {
            new SetColumnSort
            {
                Sort = sort.Select(a => new KeyValuePair<long, int>(a.Id, a.Value)).ToArray(),
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

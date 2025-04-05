using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Table;
using Basic.FormRemoteModel.Table.Model;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Services
{
    internal class TableService : ITableService
    {
        public long Add ( CustomTableAdd datum )
        {
            return new AddTable
            {
                Datum = datum,
            }.Send();
        }

        public void Delete ( long id )
        {
            new DeleteTable
            {
                Id = id,
            }.Send();
        }

        public CustomTable Get ( long id )
        {
            return new GetTable
            {
                Id = id,
            }.Send();
        }

        public bool Set ( long id, CustomTableSet datum )
        {
            return new SetTable
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public void SetSort ( LongNullParam<int>[] sort )
        {
            new SetTableSort
            {
                Sort = sort.Select(x => new KeyValuePair<long, int>(x.Id, x.Value)).ToArray(),
            }.Send();
        }

    }
}

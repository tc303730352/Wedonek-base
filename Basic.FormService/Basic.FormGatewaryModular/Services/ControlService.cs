using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Control;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormGatewaryModular.Services
{
    internal class ControlService : IControlService
    {
        public long Add ( ControlAdd data )
        {
            return new AddControl
            {
                Datum = data
            }.Send();
        }

        public void Delete ( long id )
        {
            new DeleteControl
            {
                Id = id
            }.Send();
        }

        public ControlDetailed Get ( long id )
        {
            return new GetControl
            {
                Id = id
            }.Send();
        }
        public PagingResult<ControlDto> Query ( ControlQuery query, IBasicPage paging )
        {
            return new QueryControl
            {
                Index = paging.Index,
                SortName = paging.SortName,
                Size = paging.Size,
                IsDesc = paging.IsDesc,
                NextId = paging.NextId,
                Query = query
            }.Send();
        }

        public bool Set ( long id, ControlSet set )
        {
            return new SetControl
            {
                Datum = set,
                Id = id
            }.Send();
        }
    }
}

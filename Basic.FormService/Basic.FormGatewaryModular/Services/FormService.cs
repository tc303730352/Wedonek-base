using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Form;
using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Services
{
    internal class FormService : IFormService
    {
        public long Add ( FormAdd datum )
        {
            return new AddForm
            {
                Datum = datum,
            }.Send();
        }

        public void Delete ( long id )
        {
            new DeleteForm
            {
                Id = id,
            }.Send();
        }

        public FormDto Get ( long id )
        {
            return new GetForm
            {
                Id = id,
            }.Send();
        }

        public PagingResult<FormDto> Query ( PagingParam<FormQuery> query )
        {
            return new QueryForm
            {
                Query = query.Query,
                Index = query.Index,
                Size = query.Size,
                NextId = query.NextId,
                SortName = query.SortName,
                IsDesc = query.IsDesc
            }.Send();
        }

        public bool Set ( long id, FormSet datum )
        {
            return new SetForm
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public void Stop ( long id )
        {
            new StopForm
            {
                Id = id,
            }.Send();
        }
        public void Enable ( long id )
        {

        }
    }
}

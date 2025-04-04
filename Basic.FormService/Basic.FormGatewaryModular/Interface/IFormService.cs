using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Interface
{
    public interface IFormService
    {
        FormBody GetBody ( long id );
        long Add ( FormAdd datum );
        void Delete ( long id );
        void Enable ( long id );
        FormDto Get ( long id );
        PagingResult<FormDto> Query ( PagingParam<FormQuery> query );
        bool Set ( long id, FormSet datum );
        void Stop ( long id );
    }
}
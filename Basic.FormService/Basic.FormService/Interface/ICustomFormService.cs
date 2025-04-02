using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormService.Interface
{
    public interface ICustomFormService
    {
        long Add ( FormAdd data );
        void Delete ( long id );
        FormDto Get ( long id );
        PagingResult<FormDto> Query ( FormQuery query, IBasicPage paging );
        bool Set ( long id, FormSet set );
        bool SetStatus ( long id, FormStatus status );
    }
}
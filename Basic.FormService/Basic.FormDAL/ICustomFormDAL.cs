using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Model;

namespace Basic.FormDAL
{
    public interface ICustomFormDAL : IBasicDAL<DBCustomForm, long>
    {
        long Add(FormAdd form);
        void Drop(long id);
        Result[] Query<Result>(FormQuery query, IBasicPage paging, out int count) where Result : class;
        void SetStatus(DBCustomForm form, FormStatus status);
    }
}
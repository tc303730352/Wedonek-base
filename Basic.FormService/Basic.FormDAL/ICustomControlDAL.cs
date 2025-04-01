using Basic.FormModel.DB;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Model;

namespace Basic.FormDAL
{
    public interface ICustomControlDAL : IBasicDAL<DBCustomControl, long>
    {
        long Add ( ControlAdd data );
        Result[] Query<Result> ( ControlQuery query, IBasicPage paging, out int count ) where Result : class;
    }
}
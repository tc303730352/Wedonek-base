using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IOperateMenuDAL : IBasicDAL<DBOperateMenu, long>
    {
        Result[] Query<Result> ( OpMenuQuery query, IBasicPage paging, out int count ) where Result : class;
        long Add ( DBOperateMenu add );
        void SetIsEnable ( DBOperateMenu menu, bool isEnable );
    }
}
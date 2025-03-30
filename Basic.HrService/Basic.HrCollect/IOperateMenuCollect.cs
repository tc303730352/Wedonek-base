using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IOperateMenuCollect
    {
        DBOperateMenu Get ( long id );
        Result[] Query<Result> ( OpMenuQuery query, IBasicPage paging, out int count ) where Result : class;
        long Add ( OpMenuAdd add );
        OperateMenu[] GetMenus ();
        bool SetIsEnable ( DBOperateMenu menu, bool isEnable );
        void Delete ( DBOperateMenu menu );
    }
}
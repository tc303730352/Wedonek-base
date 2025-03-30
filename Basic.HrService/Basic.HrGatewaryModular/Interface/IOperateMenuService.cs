using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperateMenuService
    {
        long Add ( OpMenuAdd add );
        void Delete ( long id );
        OperateMenuDto Get ( long id );
        PagingResult<OperateMenuDto> Query ( PagingParam<OpMenuQuery> param );
        bool SetIsEnable ( long id, bool isEnable );
    }
}
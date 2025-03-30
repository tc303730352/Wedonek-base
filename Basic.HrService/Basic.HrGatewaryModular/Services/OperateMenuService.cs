using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OpMenu;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class OperateMenuService : IOperateMenuService
    {
        public long Add ( OpMenuAdd add )
        {
            return new AddOperateMenu
            {
                Datum = add
            }.Send();
        }
        public bool SetIsEnable ( long id, bool isEnable )
        {
            return new SetOperateMenuState
            {
                Id = id,
                IsEnable = isEnable
            }.Send();
        }
        public void Delete ( long id )
        {
            new DeleteOperateMenu
            {
                Id = id
            }.Send();
        }

        public OperateMenuDto Get ( long id )
        {
            return new GetOperateMenu
            {
                Id = id
            }.Send();
        }

        public PagingResult<OperateMenuDto> Query ( PagingParam<OpMenuQuery> param )
        {
            return new QueryOperateMenu
            {
                Query = param.Query,
                Size = param.Size,
                SortName = param.SortName,
                Index = param.Index,
                IsDesc = param.IsDesc,
                NextId = param.NextId
            }.Send();
        }
    }
}

using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class OperateMenuApi : ApiController
    {
        private readonly IOperateMenuService _Service;

        public OperateMenuApi ( IOperateMenuService service )
        {
            this._Service = service;
        }
        [ApiPower("all", "hr.operate.menu.add")]
        public long Add ( OpMenuAdd add )
        {
            return this._Service.Add(add);
        }
        [ApiPower("all", "hr.operate.menu.delete")]
        public void Delete ( [NumValidate("hr.menu.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }

        public OperateMenuDto Get ( [NumValidate("hr.menu.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }

        public PagingResult<OperateMenuDto> Query ( PagingParam<OpMenuQuery> param )
        {
            return this._Service.Query(param);
        }
        [ApiPower("all", "hr.operate.menu.set")]
        public bool SetIsEnable ( LongParam<bool> param )
        {
            return this._Service.SetIsEnable(param.Id, param.Value);
        }
    }
}

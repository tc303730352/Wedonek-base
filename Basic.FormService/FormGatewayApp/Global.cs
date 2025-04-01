using Basic.FormGatewaryModular;
using WeDonekRpc.ApiGateway;
using WeDonekRpc.ApiGateway.Interface;
using WeDonekRpc.HttpApiDoc;

namespace FormGatewayApp
{
    internal class Global : BasicGlobal
    {
        public override void Load ( IGatewayOption option )
        {
            //注册业务接口模块
            option.RegModular(new FormGatewaryApiModular());
            //注册在线文档模块
            option.RegDoc(new ApiDocModular());
        }
    }
}

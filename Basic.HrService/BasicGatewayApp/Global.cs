using Basic.HrGatewaryModular;
using WeDonekRpc.ApiGateway;
using WeDonekRpc.ApiGateway.Interface;
using WeDonekRpc.HttpApiDoc;

namespace BasicGatewayApp
{
    internal class Global : BasicGlobal
    {
        public override void Load (IGatewayOption option)
        {
            //注册业务接口模块
            option.RegModular(new HrGatewaryApiModular());
            //注册在线文档模块
            option.RegDoc(new ApiDocModular());
        }
    }
}

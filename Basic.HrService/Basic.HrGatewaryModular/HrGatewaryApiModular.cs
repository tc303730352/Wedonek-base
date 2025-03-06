using WeDonekRpc.Client.Attr;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;

namespace Basic.HrGatewaryModular
{
    public class HrGatewaryApiModular : BasicApiModular
    {
        public HrGatewaryApiModular () : base("HrApi_Gateway")
        {
        }
        protected override void Load (IHttpGatewayOption option, IModularConfig config)
        {
            config.ApiRouteFormat = "/hr/{controller}/{name}";
            option.IocBuffer.SetDefLifetimeType((body) =>
            {
                if (body.To.FullName.StartsWith("Basic.HrGatewaryModular.Interface."))
                {
                    body.SetLifetimeType(ClassLifetimeType.SingleInstance);
                }
            });
        }
    }
}
